using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;
using Server.Accounting;
using Server.Engines.ConPVP;

namespace Server.Engines.Reports
{
    public class Reports
	{
		public static bool Enabled = false;

		public static void Initialize()
		{
			if ( !Enabled )
				return;

			m_StatsHistory = new SnapshotHistory();
			m_StatsHistory.Load();

			m_StaffHistory = new StaffHistory();
			m_StaffHistory.Load();

			DateTime now = DateTime.Now;

			DateTime date = now.Date;
			TimeSpan timeOfDay = now.TimeOfDay;

			m_GenerateTime = date + TimeSpan.FromHours( Math.Ceiling( timeOfDay.TotalHours ) );

			Timer.DelayCall( TimeSpan.FromMinutes( 0.5 ), TimeSpan.FromMinutes( 0.5 ), new TimerCallback( CheckRegenerate ) );
		}

		private static DateTime m_GenerateTime;

		public static void CheckRegenerate()
		{
			if ( DateTime.Now < m_GenerateTime )
				return;

			Generate();
			m_GenerateTime += TimeSpan.FromHours( 1.0 );
		}

		private static SnapshotHistory m_StatsHistory;
		private static StaffHistory m_StaffHistory;

		public static StaffHistory StaffHistory{ get{ return m_StaffHistory; } }

		public static void Generate()
		{
			Snapshot ss = new Snapshot();

			ss.TimeStamp = DateTime.Now;

			FillSnapshot( ss );

			m_StatsHistory.Snapshots.Add( ss );
			m_StaffHistory.QueueStats.Add( new QueueStatus( Engines.Help.PageQueue.List.Count ) );

			ThreadPool.QueueUserWorkItem( new WaitCallback( UpdateOutput ), ss );
		}

		private static void UpdateOutput( object state )
		{
			m_StatsHistory.Save();
			m_StaffHistory.Save();

			HtmlRenderer renderer = new HtmlRenderer( "stats", (Snapshot) state, m_StatsHistory );
			renderer.Render();
			renderer.Upload();

			renderer = new HtmlRenderer( "staff", m_StaffHistory );
			renderer.Render();
			renderer.Upload();
		}

		public static void FillSnapshot( Snapshot ss )
		{
			ss.Children.Add( CompileGeneralStats() );
			ss.Children.Add( CompilePCByDL() );
			ss.Children.Add( CompileTop15() );
			ss.Children.Add( CompileDislikedArenas() );
			ss.Children.Add( CompileStatChart() );

			PersistableObject[] obs = CompileSkillReports();

			for ( int i = 0; i < obs.Length; ++i )
				ss.Children.Add( obs[i] );
		}

		public static Report CompileGeneralStats()
		{
			Report report = new Report( "General Stats", "200" );

			report.Columns.Add( "50%", "left" );
			report.Columns.Add( "50%", "left" );

			int npcs = 0, players = 0;

			foreach ( Mobile mob in World.Mobiles.Values )
			{
				if ( mob.Player )
					++players;
				else
					++npcs;
			}

			report.Items.Add( "NPCs", npcs, "N0" );
			report.Items.Add( "Players", players, "N0" );
			report.Items.Add( "Clients", NetState.Instances.Count, "N0" );
			report.Items.Add( "Accounts", Accounts.Count, "N0" );
			report.Items.Add( "Items", World.Items.Count, "N0" );

			return report;
		}

		private static Chart CompilePCByDL()
		{
			BarGraph chart = new BarGraph( "Player Count By Dueling Level", "graphs_pc_by_dl", 5, "Dueling Level", "Players", BarGraphRenderMode.Bars );

			int lastLevel = -1;
			ChartItem lastItem = null;

			Ladder ladder = Ladder.Instance;

			if ( ladder != null )
			{
				ArrayList entries = ladder.ToArrayList();

				for ( int i = entries.Count - 1; i >= 0; --i )
				{
					LadderEntry entry = (LadderEntry)entries[i];
					int level = Ladder.GetLevel( entry.Experience );

					if ( lastItem == null || level != lastLevel )
					{
						chart.Items.Add( lastItem = new ChartItem( level.ToString(), 1 ) );
						lastLevel = level;
					}
					else
					{
						lastItem.Value++;
					}
				}
			}

			return chart;
		}

		private static Report CompileTop15()
		{
			Report report = new Report( "Top 15 Duelists", "80%" );

			report.Columns.Add( "6%", "center", "Rank" );
			report.Columns.Add( "6%", "center", "Level" );
			report.Columns.Add( "6%", "center", "Guild" );
			report.Columns.Add( "70%", "left", "Name" );
			report.Columns.Add( "6%", "center", "Wins" );
			report.Columns.Add( "6%", "center", "Losses" );

			Ladder ladder = Ladder.Instance;

			if ( ladder != null )
			{
				ArrayList entries = ladder.ToArrayList();

				for ( int i = 0; i < entries.Count && i < 15; ++i )
				{
					LadderEntry entry = (LadderEntry)entries[i];
					int level = Ladder.GetLevel( entry.Experience );
					string guild = "";

					if ( entry.Mobile.Guild != null )
						guild = entry.Mobile.Guild.Abbreviation;

					ReportItem item = new ReportItem();

					item.Values.Add( LadderGump.Rank( entry.Index + 1 ) );
					item.Values.Add( level.ToString(), "N0" );
					item.Values.Add( guild );
					item.Values.Add( entry.Mobile.Name );
					item.Values.Add( entry.Wins.ToString(), "N0" );
					item.Values.Add( entry.Losses.ToString(), "N0" );

					report.Items.Add( item );
				}
			}

			return report;
		}

		private static Chart CompileDislikedArenas()
		{
			PieChart chart = new PieChart( "Most Disliked Arenas", "graphs_arenas_disliked", false );

			Preferences prefs = Preferences.Instance;

			if ( prefs != null )
			{
				List<Arena> arenas = Arena.Arenas;

				for ( int i = 0; i < arenas.Count; ++i )
				{
					Arena arena = arenas[i];

					string name = arena.Name;

					if ( name != null )
						chart.Items.Add( name, 0 );
				}

				ArrayList entries = prefs.Entries;

				for ( int i = 0; i < entries.Count; ++i )
				{
					PreferencesEntry entry = (PreferencesEntry)entries[i];
					ArrayList list = entry.Disliked;

					for ( int j = 0; j < list.Count; ++j )
					{
						string disliked = (string)list[j];

						for ( int k = 0; k < chart.Items.Count; ++k )
						{
							ChartItem item = chart.Items[k];

							if ( item.Name == disliked )
							{
								++item.Value;
								break;
							}
						}
					}
				}
			}

			return chart;
		}

		public static Chart CompileStatChart()
		{
			PieChart chart = new PieChart( "Stat Distribution", "graphs_strdexint_distrib", true );

			ChartItem strItem = new ChartItem( "Strength", 0 );
			ChartItem dexItem = new ChartItem( "Dexterity", 0 );
			ChartItem intItem = new ChartItem( "Intelligence", 0 );

			foreach ( Mobile mob in World.Mobiles.Values )
			{
				if ( mob.RawStatTotal == mob.StatCap && mob is PlayerMobile )
				{
					strItem.Value += mob.RawStr;
					dexItem.Value += mob.RawDex;
					intItem.Value += mob.RawInt;
				}
			}

			chart.Items.Add( strItem );
			chart.Items.Add( dexItem );
			chart.Items.Add( intItem );

			return chart;
		}

		public class SkillDistribution : IComparable
		{
			public SkillInfo m_Skill;
			public int m_NumberOfGMs;

			public SkillDistribution( SkillInfo skill )
			{
				m_Skill = skill;
			}

			public int CompareTo( object obj )
			{
				return ((SkillDistribution)obj).m_NumberOfGMs - m_NumberOfGMs;
			}
		}

		public static SkillDistribution[] GetSkillDistribution()
		{
			int skip = 6;

			SkillDistribution[] distribs = new SkillDistribution[SkillInfo.Table.Length - skip];

			for ( int i = 0; i < distribs.Length; ++i )
				distribs[i] = new SkillDistribution( SkillInfo.Table[i] );

			foreach ( Mobile mob in World.Mobiles.Values )
			{
				if ( mob.SkillsTotal >= 1500 && mob.SkillsTotal <= 7200 && mob is PlayerMobile )
				{
					Skills skills = mob.Skills;

					for ( int i = 0; i < skills.Length - skip; ++i )
					{
						Skill skill = skills[i];

						if ( skill.BaseFixedPoint >= 1000 )
							distribs[i].m_NumberOfGMs++;
					}
				}
			}

			return distribs;
		}

		public static PersistableObject[] CompileSkillReports()
		{
			SkillDistribution[] distribs = GetSkillDistribution();

			Array.Sort( distribs );

			return new PersistableObject[]{ CompileSkillChart( distribs ), CompileSkillReport( distribs ) };
		}

		public static Report CompileSkillReport( SkillDistribution[] distribs )
		{
			Report report = new Report( "Skill Report", "300" );

			report.Columns.Add( "70%", "left", "Name" );
			report.Columns.Add( "30%", "center", "GMs" );

			for ( int i = 0; i < distribs.Length; ++i )
				report.Items.Add( distribs[i].m_Skill.Name, distribs[i].m_NumberOfGMs, "N0" );

			return report;
		}

		public static Chart CompileSkillChart( SkillDistribution[] distribs )
		{
			PieChart chart = new PieChart( "GM Skill Distribution", "graphs_skill_distrib", true );

			for ( int i = 0; i < 12; ++i )
				chart.Items.Add( distribs[i].m_Skill.Name, distribs[i].m_NumberOfGMs );

			int rem = 0;

			for ( int i = 12; i < distribs.Length; ++i )
				rem += distribs[i].m_NumberOfGMs;

			chart.Items.Add( "Other", rem );

			return chart;
		}
	}
}
