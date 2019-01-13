/***************************************************************************
 *                                  Race.cs
 *                            -------------------
 *   begin                : May 1, 2002
 *   copyright            : (C) The RunUO Software Team
 *   email                : info@runuo.com
 *
 *   $Id: Race.cs 644 2010-12-23 09:18:45Z asayre $
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

using System;
using System.Collections.Generic;

namespace Server
{
    [Parsable]
	public abstract class Race
	{
		public static Race DefaultRace { get { return m_Races[0];  } }

		private static Race[] m_Races = new Race[0x100];

		public static Race[] Races { get { return m_Races; } }

		public static Race Human { get { return m_Races[0]; } }
		public static Race Elf { get { return m_Races[1]; } }

		private static List<Race> m_AllRaces = new List<Race>();

		public static List<Race> AllRaces { get { return m_AllRaces; } }

		private int m_RaceID, m_RaceIndex;

		private string m_Name, m_PluralName;

		public override string ToString()
		{
			return m_Name;
		}

		private int m_MaleBody, m_FemaleBody, m_MaleGhostBody, m_FemaleGhostBody;

		private Expansion m_RequiredExpansion;

		protected Race( int raceID, int raceIndex, string name, string pluralName, int maleBody, int femaleBody, int maleGhostBody, int femaleGhostBody, Expansion requiredExpansion )
		{
			m_RaceID = raceID;
			m_RaceIndex = raceIndex;

			m_Name = name;

			m_MaleBody = maleBody;
			m_FemaleBody = femaleBody;
			m_MaleGhostBody = maleGhostBody;
			m_FemaleGhostBody = femaleGhostBody;

			m_RequiredExpansion = requiredExpansion;
			m_PluralName = pluralName;
		}

		public virtual bool ValidateHair( Mobile m, int itemID ) { return ValidateHair( m.Female, itemID ); }
		public abstract bool ValidateHair( bool female, int itemID );

		public virtual int RandomHair( Mobile m ) { return RandomHair( m.Female ); }
		public abstract int RandomHair( bool female );

		public virtual bool ValidateFacialHair( Mobile m, int itemID ) { return ValidateFacialHair( m.Female, itemID ); }
		public abstract bool ValidateFacialHair( bool female, int itemID );

		public virtual int RandomFacialHair( Mobile m ) { return RandomFacialHair( m.Female ); }
		public abstract int RandomFacialHair( bool female );	//For the *ahem* bearded ladies

		public abstract int ClipSkinHue( int hue );
		public abstract int RandomSkinHue();

		public abstract int ClipHairHue( int hue );
		public abstract int RandomHairHue();

		public virtual int Body( Mobile m )
		{
			if( m.Alive )
				return AliveBody( m.Female );

			return GhostBody( m.Female );
		}

		public virtual int AliveBody( Mobile m ) { return AliveBody( m.Female ); }
		public virtual int AliveBody( bool female )
		{
			return female ? m_FemaleBody : m_MaleBody;
		}

		public virtual int GhostBody( Mobile m ) { return GhostBody( m.Female ); }
		public virtual int GhostBody( bool female )
		{
			return female ? m_FemaleGhostBody : m_MaleGhostBody;
		}

		public int RaceIndex
		{
			get
			{
				return m_RaceIndex;
			}
		}

		public string PluralName
		{
			get
			{
				return m_PluralName;
			}
			set
			{
				m_PluralName = value;
			}
		}
	}
}