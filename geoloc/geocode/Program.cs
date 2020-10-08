using NGeoNames;
using NGeoNames.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Globalization;
using System.Resources;

namespace geocode
{
    class Program
    {
        static readonly IEnumerable<ExtendedGeoName> _locationNames;
        static readonly ReverseGeoCode<ExtendedGeoName> _reverseGeoCodingService;
        static readonly CultureInfo _formatProvider;

        static Program()
        {
            _locationNames = GeoFileReader.ReadExtendedGeoNames(".\\Resources\\SE.txt");
            _reverseGeoCodingService = new ReverseGeoCode<ExtendedGeoName>(_locationNames);

            _formatProvider = CultureInfo.InvariantCulture;
        }

        static void Main(string[] args)
        {
            // 1. Hitta de 10 närmsta platserna till Gävle (60.674622, 17.141830), sorterat på namn.
            ListGavleLocations();

            // 2. Hitta alla platser inom 200 km radie till Uppsala (59.858562, 17.638927), sorterat på avstånd.
            ListUppsalaLocations();

            // 3. Lista x platser baserat på användarinmatning.
            Console.WriteLine("User: ");
            Console.WriteLine("----------");
            ListUserPositions(args);
            
        }

        static void ListUserPositions(string[] args)
        {
            double lat = double.Parse(args[0], _formatProvider);
            double lng = double.Parse(args[1], _formatProvider);

            var nearestUser = _reverseGeoCodingService.RadialSearch(lat, lng, 10);

            foreach (var position in nearestUser)
            {
                var userDistance = position.DistanceTo(lat, lng);

                Console.WriteLine($"{position.Name}, distance: {userDistance}");
            }
        }

        static void ListUppsalaLocations()
        {
            var uppsala = _reverseGeoCodingService.CreateFromLatLong(59.858562, 17.638927);
            var locsWithin200KmOfUppsala = _reverseGeoCodingService.RadialSearch(uppsala, 200_000, 50);
            //locsWithin200KmOfUppsala = locsWithin200KmOfUppsala.OrderBy(p => p.DistanceTo(uppsala));

            Console.WriteLine("\nSites within 200 Km of Uppsala:");
            ListLocations(locsWithin200KmOfUppsala);
        }

        static void ListGavleLocations()
        {
            var gavle = _reverseGeoCodingService.CreateFromLatLong(60.674622, 17.141830);
            List<GeoName> nearestGavle = _reverseGeoCodingService.NearestNeighbourSearch(gavle, 10).ToList<GeoName>();
            nearestGavle.Sort(compareGeos);
            //nearestGavle = nearestGavle.OrderBy(p => p.Name);

            Console.WriteLine("10 Nearest Gävle:");
            ListLocations(nearestGavle);
        }

        static void ListLocations(IEnumerable<GeoName> list)
        {
            foreach (var loc in list)
            {
                Console.WriteLine(loc.Name + " " + loc.Latitude + ", " + loc.Longitude);
            }
        }

        static int compareGeos(GeoName g1, GeoName g2)
        {
            CultureInfo ci = new CultureInfo("sv-SE");

            if (g1 == null)
            {
                if (g2 == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (g2 == null)
                {
                    return 1;
                }
                else
                {
                    int res = string.Compare(g1.Name, g2.Name, true, ci);

                    if (res != 0)
                    {
                        return res;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }



        }
    }
}
