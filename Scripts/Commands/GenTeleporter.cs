using System.Collections;
using Server.Items;

namespace Server.Commands
{
    public class GenTeleporter
	{
		public GenTeleporter()
		{
		}

		public static void Initialize()
		{
			CommandSystem.Register( "TelGen", AccessLevel.Administrator, new CommandEventHandler( GenTeleporter_OnCommand ) );
		}

		[Usage( "TelGen" )]
		[Description( "Generates world/dungeon teleporters for all facets." )]
		public static void GenTeleporter_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendMessage( "Generating teleporters, please wait." );

			int count = new TeleportersCreator().CreateTeleporters();

			count += new SHTeleporter.SHTeleporterCreator().CreateSHTeleporters();

			e.Mobile.SendMessage( "Teleporter generating complete. {0} teleporters were generated.", count );
		}

		public class TeleportersCreator
		{
			private int m_Count;
			
			public TeleportersCreator()
			{
			}

			private static Queue m_Queue = new Queue();

			public static bool FindTeleporter( Map map, Point3D p )
			{
				IPooledEnumerable eable = map.GetItemsInRange( p, 0 );

				foreach ( Item item in eable )
				{
					if ( item is Teleporter && !(item is KeywordTeleporter) && !(item is SkillTeleporter) )
					{
						int delta = item.Z - p.Z;

						if ( delta >= -12 && delta <= 12 )
							m_Queue.Enqueue( item );
					}
				}

				eable.Free();

				while ( m_Queue.Count > 0 )
					((Item)m_Queue.Dequeue()).Delete();

				return false;
			}

			public void CreateTeleporter( Point3D pointLocation, Point3D pointDestination, Map mapLocation, Map mapDestination, bool back )
			{
				if ( !FindTeleporter( mapLocation, pointLocation ) )
				{
					m_Count++;
				
					Teleporter tel = new Teleporter( pointDestination, mapDestination );

					tel.MoveToWorld( pointLocation, mapLocation );
				}

				if ( back && !FindTeleporter( mapDestination, pointDestination ) )
				{
					m_Count++;

					Teleporter telBack = new Teleporter( pointLocation, mapLocation );

					telBack.MoveToWorld( pointDestination, mapDestination );
				}
			}

			public void CreateTeleporter( int xLoc, int yLoc, int zLoc, int xDest, int yDest, int zDest, Map map, bool back )
			{
				CreateTeleporter( new Point3D( xLoc, yLoc, zLoc ), new Point3D( xDest, yDest, zDest ), map, map, back);
			}

			public void CreateTeleporter( int xLoc, int yLoc, int zLoc, int xDest, int yDest, int zDest, Map mapLocation, Map mapDestination, bool back )
			{
				CreateTeleporter( new Point3D( xLoc, yLoc, zLoc ), new Point3D( xDest, yDest, zDest ), mapLocation, mapDestination, back);
			}

			public void DestroyTeleporter( int x, int y, int z, Map map )
			{
				Point3D p = new Point3D( x, y, z );
				IPooledEnumerable eable = map.GetItemsInRange( p, 0 );

				foreach ( Item item in eable )
				{
					if ( item is Teleporter && !(item is KeywordTeleporter) && !(item is SkillTeleporter) && item.Z == p.Z )
						m_Queue.Enqueue( item );
				}

				eable.Free();

				while ( m_Queue.Count > 0 )
					((Item)m_Queue.Dequeue()).Delete();
			}

			public void CreateTeleportersMap( Map map )
			{
				// Shame
				CreateTeleporter( 512, 1559, 0, 5394, 127, 0, map, true );
				CreateTeleporter( 513, 1559, 0, 5395, 127, 0, map, true );
				CreateTeleporter( 514, 1559, 0, 5396, 127, 0, map, true );
				CreateTeleporter( 5490, 19, -25, 5514, 10, 5, map, true );
				CreateTeleporter( 5875, 19, -5, 5507, 162, 5, map, true );
				CreateTeleporter( 5604, 102, 5, 5514, 147, 25, map, true );

				CreateTeleporter( 5513, 176, 5, 5540, 187, 0, map, false );
				CreateTeleporter( 5538, 170, 5, 5517, 176, 0, map, false );
				
				// Hythloth
				CreateTeleporter( 4721, 3813, 0, 5904, 16, 64, map, true );
				CreateTeleporter( 4722, 3813, 0, 5905, 16, 64, map, true );
				CreateTeleporter( 4723, 3813, 0, 5906, 16, 64, map, true );
				CreateTeleporter( 6040, 192, 12, 6059, 88, 24, map, true );
				CreateTeleporter( 6040, 193, 12, 6059, 89, 24, map, true );
				CreateTeleporter( 6040, 194, 12, 6059, 90, 24, map, true );

				DestroyTeleporter( 5920, 168, 16, map );
				DestroyTeleporter( 5920, 169, 17, map );
				DestroyTeleporter( 5920, 170, 16, map );

				CreateTeleporter( 5920, 168, 16, 6083, 144, -20, map, false );
				CreateTeleporter( 5920, 169, 16, 6083, 145, -20, map, false );
				CreateTeleporter( 5920, 170, 16, 6083, 146, -20, map, false );

				CreateTeleporter( 6083, 144, -20, 5920, 168, 22, map, false );
				CreateTeleporter( 6083, 145, -20, 5920, 169, 22, map, false );
				CreateTeleporter( 6083, 146, -20, 5920, 170, 22, map, false );

				DestroyTeleporter( 5906, 96, 0, map );

				CreateTeleporter( 5972, 168, 0, 5905, 100, 0, map, false ); 
				CreateTeleporter( 5906, 96, 0, 5977, 169, 0, map, false );

				// Covetous
				CreateTeleporter( 2498, 916, 0, 5455, 1864, 0, map, true );
				CreateTeleporter( 2499, 916, 0, 5456, 1864, 0, map, true );
				CreateTeleporter( 2500, 916, 0, 5457, 1864, 0, map, true );
				CreateTeleporter( 2384, 836, 0, 5615, 1996, 0, map, true );
				CreateTeleporter( 2384, 837, 0, 5615, 1997, 0, map, true );
				CreateTeleporter( 2384, 838, 0, 5615, 1998, 0, map, true );
				CreateTeleporter( 2420, 883, 0, 5392, 1959, 0, map, true );
				CreateTeleporter( 2421, 883, 0, 5393, 1959, 0, map, true );
				CreateTeleporter( 2422, 883, 0, 5394, 1959, 0, map, true );
				CreateTeleporter( 2455, 858, 0, 5388, 2027, 0, map, true );
				CreateTeleporter( 2456, 858, 0, 5389, 2027, 0, map, true );
				CreateTeleporter( 2457, 858, 0, 5390, 2027, 0, map, true );
				CreateTeleporter( 2544, 850, 0, 5578, 1927, 0, map, true );
				CreateTeleporter( 2545, 850, 0, 5579, 1927, 0, map, true );
				CreateTeleporter( 2546, 850, 0, 5580, 1927, 0, map, true );

				CreateTeleporter( 5551, 1805, 12, 5556, 1825, -3, map, false );
				CreateTeleporter( 5552, 1805, 12, 5557, 1825, -3, map, false );
				CreateTeleporter( 5553, 1805, 12, 5557, 1825, -3, map, false );

				DestroyTeleporter( 5551, 1807, 0, map );
				DestroyTeleporter( 5552, 1807, 0, map );
				DestroyTeleporter( 5553, 1807, 0, map );

				CreateTeleporter( 5556, 1826, -10, 5551, 1806, 7, map, false );
				CreateTeleporter( 5557, 1826, -10, 5552, 1806, 7, map, false );

				DestroyTeleporter( 5556, 1825, -7, map );
				DestroyTeleporter( 5556, 1827, -13, map );
				DestroyTeleporter( 5557, 1825, -7, map );
				DestroyTeleporter( 5557, 1827, -13, map );
				DestroyTeleporter( 5558, 1825, -7, map );

				DestroyTeleporter( 5468, 1804, 0, map );
				DestroyTeleporter( 5468, 1805, 0, map );
				DestroyTeleporter( 5468, 1806, 0, map );

				CreateTeleporter( 5466, 1804, 12, 5593, 1840, -3, map, false );
				CreateTeleporter( 5466, 1805, 12, 5593, 1841, -3, map, false );
				CreateTeleporter( 5466, 1806, 12, 5593, 1842, -3, map, false );

				DestroyTeleporter( 5595, 1840, -14, map );
				DestroyTeleporter( 5595, 1840, -14, map );

				CreateTeleporter( 5594, 1840, -9, 5467, 1804, 7, map, false );
				CreateTeleporter( 5594, 1841, -9, 5467, 1805, 7, map, false );

				// Wrong
				CreateTeleporter( 5824, 631, 5, 2041, 215, 14, map, true );
				CreateTeleporter( 5825, 631, 5, 2042, 215, 14, map, true );
				CreateTeleporter( 5825, 631, 5, 2043, 215, 14, map, true );
				CreateTeleporter( 5698, 662, 0, 5793, 527, 10, map, false );

				DestroyTeleporter( 5863, 525, 15, map );
				DestroyTeleporter( 5863, 526, 15, map );
				DestroyTeleporter( 5863, 527, 15, map );
				DestroyTeleporter( 5868, 537, 15, map );
				DestroyTeleporter( 5868, 538, 15, map );
				DestroyTeleporter( 5869, 538, 15, map );
				DestroyTeleporter( 5733, 554, 20, map );
				DestroyTeleporter( 5862, 527, 15, map );

				// Deceit
				CreateTeleporter( 4110, 430, 5, 5187, 639, 0, map, false );
				CreateTeleporter( 4111, 430, 5, 5188, 639, 0, map, false );
				CreateTeleporter( 4112, 430, 5, 5189, 639, 0, map, false );
				CreateTeleporter( 5187, 639, 0, 4110, 430, 5, map, false );
				CreateTeleporter( 5188, 639, 0, 4111, 430, 5, map, false );
				CreateTeleporter( 5189, 639, 0, 4112, 430, 5, map, false );
				CreateTeleporter( 5216, 586, -13, 5304, 533, 2, map, false );
				CreateTeleporter( 5217, 586, -13, 5305, 533, 2, map, false );
				CreateTeleporter( 5218, 586, -13, 5306, 533, 2, map, false );
				CreateTeleporter( 5304, 532, 7, 5216, 585, -8, map, false );
				CreateTeleporter( 5305, 532, 7, 5217, 585, -8, map, false );
				CreateTeleporter( 5306, 532, 7, 5218, 585, -8, map, false );
				CreateTeleporter( 5218, 761, -28, 5305, 651, 7, map, false );
				CreateTeleporter( 5219, 761, -28, 5306, 651, 7, map, false );
				CreateTeleporter( 5305, 650, 12, 5218, 760, -23, map, false );
				CreateTeleporter( 5306, 650, 12, 5219, 760, -23, map, false );
				CreateTeleporter( 5346, 578, 5, 5137, 649, 5, map, true );

				CreateTeleporter( 5186, 639, 0, 4110, 430, 5, map, false );

				// Despise
				CreateTeleporter( 5504, 569, 46, 5574, 628, 37, map, false );
				CreateTeleporter( 5504, 570, 46, 5574, 629, 37, map, false );
				CreateTeleporter( 5504, 571, 46, 5574, 630, 37, map, false );
				CreateTeleporter( 5572, 632, 17, 5521, 672, 27, map, false );
				CreateTeleporter( 5572, 633, 17, 5521, 673, 27, map, false );
				CreateTeleporter( 5572, 634, 17, 5521, 674, 27, map, false );
				CreateTeleporter( 5573, 628, 42, 5503, 569, 51, map, false );
				CreateTeleporter( 5573, 629, 42, 5503, 570, 51, map, false );
				CreateTeleporter( 5573, 630, 42, 5503, 571, 51, map, false );
				CreateTeleporter( 5588, 632, 30, 1296, 1082, 0, map, true );
				CreateTeleporter( 5588, 630, 30, 1296, 1080, 0, map, true );
				CreateTeleporter( 5588, 631, 30, 1296, 1081, 0, map, true );
				CreateTeleporter( 5522, 672, 32, 5573, 632, 22, map, false );
				CreateTeleporter( 5522, 673, 32, 5573, 633, 22, map, false );
				CreateTeleporter( 5522, 674, 32, 5573, 634, 22, map, false );
				CreateTeleporter( 5386, 756, -8, 5408, 859, 47, map, false );
				CreateTeleporter( 5386, 757, -8, 5408, 860, 47, map, false );
				CreateTeleporter( 5386, 755, -8, 5408, 858, 47, map, false );
				CreateTeleporter( 5409, 860, 52, 5387, 757, -3, map, false );
				CreateTeleporter( 5409, 858, 52, 5387, 755, -3, map, false );
				CreateTeleporter( 5409, 859, 52, 5387, 756, -3, map, false );

				// Destard
				CreateTeleporter( 5242, 1007, 0, 1175, 2635, 0, map, true );
				CreateTeleporter( 5243, 1007, 0, 1176, 2635, 0, map, true );
				CreateTeleporter( 5244, 1007, 0, 1177, 2635, 0, map, true );
				CreateTeleporter( 5142, 797, 22, 5129, 908, -23, map, false );
				CreateTeleporter( 5143, 797, 22, 5130, 908, -23, map, false );
				CreateTeleporter( 5144, 797, 22, 5131, 908, -23, map, false );
				CreateTeleporter( 5145, 797, 22, 5132, 908, -23, map, false );
				CreateTeleporter( 5153, 808, -25, 5134, 984, 17, map, false );
				CreateTeleporter( 5153, 809, -25, 5134, 985, 17, map, false );
				CreateTeleporter( 5153, 810, -25, 5134, 986, 17, map, false );
				CreateTeleporter( 5153, 811, -25, 5134, 987, 17, map, false );
				CreateTeleporter( 5129, 909, -28, 5142, 798, 17, map, false );
				CreateTeleporter( 5130, 909, -28, 5143, 798, 17, map, false );
				CreateTeleporter( 5131, 909, -28, 5144, 798, 17, map, false );
				CreateTeleporter( 5132, 909, -28, 5145, 798, 17, map, false );
				CreateTeleporter( 5133, 984, 22, 5152, 808, -19, map, false );
				CreateTeleporter( 5133, 985, 22, 5152, 809, -19, map, false );
				CreateTeleporter( 5133, 986, 22, 5152, 810, -19, map, false );
				CreateTeleporter( 5133, 987, 22, 5152, 811, -19, map, false );

				// Buccaneer's Den underground tunnels

				DestroyTeleporter( 2666, 2073,   5, map );
				DestroyTeleporter( 2669, 2072, -20, map );
				DestroyTeleporter( 2669, 2073, -20, map );
				DestroyTeleporter( 2649, 2194,   4, map );
				DestroyTeleporter( 2649, 2195, -14, map );

				CreateTeleporter( 2603, 2121, -20, 2605, 2130, 8, map, false ); 
				CreateTeleporter( 2603, 2120, -20, 2605, 2130, 8, map, false ); 
				CreateTeleporter( 2669, 2071, -20, 2666, 2099, 3, map, false ); 
				CreateTeleporter( 2669, 2072, -20, 2666, 2099, 3, map, false ); 
				CreateTeleporter( 2669, 2073, -20, 2666, 2099, 3, map, false ); 
				CreateTeleporter( 2676, 2241, -18, 2691, 2234, 2, map, false ); 
				CreateTeleporter( 2676, 2242, -18, 2691, 2234, 2, map, false ); 
				CreateTeleporter( 2758, 2092, -20, 2756, 2097, 38, map, false ); 
				CreateTeleporter( 2759, 2092, -20, 2756, 2097, 38, map, false ); 
				CreateTeleporter( 2685, 2063, 39, 2685, 2063, -20, map, false ); // that should not be a teleporter: on OSI you simply fall under the ground 

				// Misc
				CreateTeleporter( 5217, 18, 15, 5204, 74, 17, map, false );
				CreateTeleporter( 5200, 71, 17, 5211, 22, 15, map, false );
				CreateTeleporter( 1997, 81, 7, 5881, 242, 0, map, false );
				CreateTeleporter( 5704, 146, -45, 5705, 305, 7, map, false );
				CreateTeleporter( 5704, 147, -45, 5705, 306, 7, map, false );
				CreateTeleporter( 5874, 146, 27, 5208, 2323, 31, map, false );
				CreateTeleporter( 5875, 146, 27, 5208, 2322, 32, map, false );
				CreateTeleporter( 5876, 146, 27, 5208, 2322, 32, map, false ); 
				CreateTeleporter( 5877, 146, 27, 5208, 2322, 32, map, false ); 
				CreateTeleporter( 5923, 169, 1, 5925, 171, 22, map, false );
				CreateTeleporter( 2399, 198, 0, 5753, 436, 79, map, false );
				CreateTeleporter( 2400, 198, 0, 5754, 436, 80, map, false );
				DestroyTeleporter( 5166, 245, 15, map );
				DestroyTeleporter( 1361, 883, 0, map );
				CreateTeleporter( 5191, 152, 0, 1367, 891, 0, map, false );
				CreateTeleporter( 5849, 239, -25, 5831, 324, 27, map, false );
				CreateTeleporter( 5850, 239, -25, 5832, 324, 26, map, false );
				CreateTeleporter( 5851, 239, -25, 5833, 324, 28, map, false );
				CreateTeleporter( 5852, 239, -25, 5834, 324, 27, map, false );
				CreateTeleporter( 5853, 239, -23, 5835, 324, 27, map, false );
				CreateTeleporter( 5882, 241, 0, 1998, 81, 5, map, false );
				CreateTeleporter( 5882, 242, 0, 1998, 81, 5, map, false );
				CreateTeleporter( 5882, 243, 0, 1998, 81, 5, map, false );
				CreateTeleporter( 5706, 305, 12, 5705, 146, -45, map, false );
				CreateTeleporter( 5706, 306, 12, 5705, 147, -45, map, false );
				CreateTeleporter( 5748, 362, 2, 313, 786, -24, map, false );
				CreateTeleporter( 5749, 362, 0, 313, 786, -24, map, false );
				CreateTeleporter( 5750, 362, 3, 314, 786, -24, map, false );
				CreateTeleporter( 5753, 324, 21, 5670, 2391, 40, map, false );
				CreateTeleporter( 5831, 323, 34, 5849, 238, -25, map, false );
				CreateTeleporter( 5832, 323, 34, 5850, 238, -25, map, false );
				CreateTeleporter( 5833, 323, 33, 5851, 238, -25, map, false );
				CreateTeleporter( 5834, 323, 33, 5852, 238, -25, map, false );
				CreateTeleporter( 5835, 323, 33, 5853, 238, -23, map, false );
				CreateTeleporter( 5658, 423, 8, 5697, 3659, 2, map, false );
				CreateTeleporter( 5686, 385, 2, 2777, 894, -23, map, false );
				CreateTeleporter( 5686, 386, 2, 2777, 894, -23, map, false );
				CreateTeleporter( 5686, 387, 2, 2777, 895, -23, map, false );
				CreateTeleporter( 5731, 445, -18, 6087, 3676, 18, map, false );
				CreateTeleporter( 5753, 437, 78, 2400, 199, 0, map, false );
				CreateTeleporter( 5850, 432, 0, 5127, 3143, 97, map, false );
				CreateTeleporter( 5850, 433, -2, 5127, 3143, 97, map, false );
				CreateTeleporter( 5850, 434, -1, 5127, 3143, 97, map, false );
				CreateTeleporter( 5850, 431, 2, 5127, 3143, 97, map, false );
				CreateTeleporter( 5826, 465, -1, 1987, 2063, -40, map, false );
				CreateTeleporter( 5827, 465, -1, 1988, 2063, -40, map, false );
				CreateTeleporter( 5828, 465, 0, 1989, 2063, -40, map, false );
				CreateTeleporter( 313, 786, -24, 5748, 361, 2, map, false );
				CreateTeleporter( 314, 786, -24, 5749, 361, 2, map, false );
				CreateTeleporter( 2776, 895, -23, 5685, 387, 2, map, false );
				//CreateTeleporter( 4545, 851, 30, 5736, 3196, 8, map, false );
				DestroyTeleporter( 4545, 851, 30, map );
				CreateTeleporter( 4540, 898, 32, 4442, 1122, 5, map, false );
				CreateTeleporter( 4300, 968, 5, 4442, 1122, 5, map, false );
				CreateTeleporter( 4436, 1107, 5, 4300, 992, 5, map, false );
				CreateTeleporter( 4443, 1137, 5, 4487, 1475, 5, map, false );
				CreateTeleporter( 4449, 1107, 5, 4539, 890, 28, map, false );
				CreateTeleporter( 4449, 1115, 5, 4671, 1135, 10, map, false );
				CreateTeleporter( 4663, 1134, 13, 4442, 1122, 5, map, false );
				CreateTeleporter( 5701, 1320, 16, 5786, 1336, -8, map, false );
				CreateTeleporter( 5702, 1320, 16, 5787, 1336, -8, map, false );
				CreateTeleporter( 5703, 1320, 16, 5788, 1336, -8, map, false );
				CreateTeleporter( 5786, 1335, -13, 5701, 1319, 13, map, false );
				CreateTeleporter( 5787, 1335, -13, 5702, 1319, 13, map, false );
				CreateTeleporter( 5788, 1335, -13, 5703, 1319, 13, map, false );
				CreateTeleporter( 6005, 1380, 1, 5151, 4063, 37, map, false );
				CreateTeleporter( 6005, 1378, 0, 5151, 4062, 37, map, false );
				CreateTeleporter( 6005, 1379, 2, 5151, 4062, 37, map, false );
				CreateTeleporter( 6025, 1344, -26, 5137, 3664, 27, map, false );
				CreateTeleporter( 6025, 1345, -26, 5137, 3664, 27, map, false );
				CreateTeleporter( 6025, 1346, -26, 5137, 3665, 31, map, false );
				CreateTeleporter( 5687, 1424, 38, 2923, 3406, 8, map, false );
				CreateTeleporter( 5792, 1416, 41, 5758, 2908, 15, map, false );
				CreateTeleporter( 5792, 1417, 41, 5758, 2909, 15, map, false );
				CreateTeleporter( 5792, 1415, 41, 5758, 2907, 15, map, false );
				CreateTeleporter( 5899, 1411, 43, 1630, 3320, 0, map, false );
				CreateTeleporter( 5900, 1411, 42, 1630, 3320, 0, map, false );
				CreateTeleporter( 5918, 1412, -29, 5961, 1409, 59, map, false );
				CreateTeleporter( 5918, 1410, -29, 5961, 1408, 59, map, false );
				CreateTeleporter( 5918, 1411, -29, 5961, 1408, 59, map, false );
				CreateTeleporter( 5961, 1408, 59, 5918, 1411, -29, map, false );
				CreateTeleporter( 5961, 1409, 59, 5918, 1412, -29, map, false );
				CreateTeleporter( 6125, 1411, 15, 6075, 3332, 4, map, false );
				CreateTeleporter( 6126, 1411, 15, 6075, 3332, 4, map, false );
				CreateTeleporter( 6127, 1411, 15, 6075, 3332, 4, map, false );
				CreateTeleporter( 6137, 1409, 2, 6140, 1432, 4, map, false );
				CreateTeleporter( 6138, 1409, 2, 6140, 1432, 4, map, false );
				CreateTeleporter( 6140, 1431, 4, 6137, 1408, 2, map, false );
				CreateTeleporter( 4496, 1475, 15, 4442, 1122, 5, map, false );
				CreateTeleporter( 6031, 1501, 42, 1491, 1642, 24, map, false );
				CreateTeleporter( 6031, 1499, 42, 1491, 1640, 24, map, false );
				CreateTeleporter( 1491, 1640, 24, 6032, 1499, 31, map, false );
				CreateTeleporter( 1491, 1642, 24, 6032, 1501, 31, map, false );
				DestroyTeleporter( 5341, 1602, 0, map );
				CreateTeleporter( 5340, 1599, 40, 5426, 3122, -74, map, false ); 
				CreateTeleporter( 5341, 1599, 40, 5426, 3122, -74, map, false );
				CreateTeleporter( 1987, 2062, -40, 5826, 464, 0, map, false );
				CreateTeleporter( 1988, 2062, -40, 5827, 464, -1, map, false );
				CreateTeleporter( 1989, 2062, -40, 5828, 464, -1, map, false );
				CreateTeleporter( 5203, 2327, 27, 5876, 147, 25, map, false );
				CreateTeleporter( 5207, 2322, 27, 5877, 147, 25, map, false );
				CreateTeleporter( 5207, 2323, 26, 5876, 147, 25, map, false );
				CreateTeleporter( 5670, 2391, 40, 5753, 325, 10, map, false );
				CreateTeleporter( 5974, 2697, 35, 2985, 2890, 35, map, false );
				CreateTeleporter( 5267, 2757, 35, 424, 3283, 35, map, false );
				CreateTeleporter( 5757, 2908, 14, 5791, 1416, 38, map, false );
				CreateTeleporter( 5757, 2909, 15, 5791, 1417, 40, map, false );
				CreateTeleporter( 5757, 2907, 15, 5791, 1415, 38, map, false );
				CreateTeleporter( 1653, 2963, 0, 1677, 2987, 0, map, false );
				CreateTeleporter( 1677, 2987, 0, 1675, 2987, 20, map, false );
				CreateTeleporter( 5426, 3123, -80, 5341, 1602, 0, map, false );
				CreateTeleporter( 5126, 3143, 99, 5849, 432, 1, map, false );
				//CreateTeleporter( 5736, 3196, 8, 4545, 851, 30, map, false );
				DestroyTeleporter( 5736, 3196, 8, map );
				CreateTeleporter( 424, 3283, 35, 5267, 2757, 35, map, false );
				CreateTeleporter( 1629, 3320, 0, 5899, 1411, 43, map, false );
				CreateTeleporter( 6075, 3332, 4, 6126, 1410, 15, map, false );
				CreateTeleporter( 2923, 3405, 6, 5687, 1423, 39, map, false );
				CreateTeleporter( 1142, 3621, 5, 1414, 3828, 5, map, false );
				CreateTeleporter( 5137, 3664, 27, 6025, 1344, -26, map, false );
				CreateTeleporter( 5137, 3665, 31, 6025, 1345, -26, map, false );
				CreateTeleporter( 5697, 3660, -5, 5658, 424, 0, map, false );
				CreateTeleporter( 6086, 3676, 16, 5731, 446, -16, map, false );
				CreateTeleporter( 1409, 3824, 5, 1124, 3623, 5, map, false );
				CreateTeleporter( 1419, 3832, 5, 1466, 4015, 5, map, false );
				CreateTeleporter( 1406, 3996, 5, 1414, 3828, 5, map, false );
				CreateTeleporter( 5150, 4062, 38, 6005, 1378, 0, map, false );
				CreateTeleporter( 5150, 4063, 38, 6005, 1380, 1, map, false );
				CreateTeleporter( 5906, 4069, 26, 2494, 3576, 5, map, true );
				CreateTeleporter( 2985, 2890, 35, 5974, 2697, 35, map, false );
			}

			public void CreateTeleportersFelucca( Map map )
			{
				// Star room
				CreateTeleporter( 5140, 1773, 0, 5171, 1586, 0, map, false );
			}

			public int CreateTeleporters()
			{
				CreateTeleportersMap( Map.Felucca );
	            CreateTeleportersFelucca( Map.Felucca );

				return m_Count;
			}
		}
	}
}
