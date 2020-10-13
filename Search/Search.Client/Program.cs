using CommandLine;
using Elasticsearch.Net;
using Search.Client.Options;
using Search.Client.Services;
using Search.Common.Models;
using Search.Common.Services;
using System;
using System.Collections.Generic;
using Error = CommandLine.Error;

namespace Search.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<SearchOptions, IndexOptions>(args)
                          .MapResult<SearchOptions, IndexOptions, object>(Search, Index, Error);
        }

        static object Search(SearchOptions options)
        {
            RecipeClient client = SearchClientFactory.CreateClient(options);

            // Denna övning använder ElasticSearch
            // https://www.elastic.co/

            // Dokumentation över hur man ställer frågor
            // https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/writing-queries.html

            // 1. Hitta 20 recept som innehåller ordet "fisk".
            // 2. Sortera sökträffarna efter rating.
            // 3. Räkna alla recept som är upplagda av Per Morberg.
            // 4. Hitta 30 recept som tillhör kategorin Bönor.
            // 5. Räkna alla recept som har en tillagningstid på under 10 minuter (tips: TimeSpan lagras som ticks i index).

            //var searchResponse1 = client.Search(s => s.QueryOnQueryString(options.Query).Take(20));

            //var searchResponse2 = client.Search(s => s.QueryOnQueryString(options.Query).Sort(o => o.Descending(d => d.Rating)));

            //var searchResponse3 = client.Search(s => s.QueryOnQueryString(options.Query).Query(q => q.Match(m => m.Field(f => f.Author))));

            // searchResponse3 = client.Search(search => search.Query(query => query.Match(match => match.Field(field => field.Author).Query(options.Query))));
            // searchResponse3 = client.Search(search => search.QueryOnQueryString("author:\"Per Morberg\""));
            //client.Count för antal

            //var searchResponse4 = client.Search(search => search.QueryOnQueryString("categories:\"Bönor\""));

            var searchResponse5 = client.Search(search => search.Query(query => query.Range(range => range.LessThan(6_000_000_000).Field(field => field.TimeToCook))));



            foreach (var doc in searchResponse5.Documents)
            {
                Console.WriteLine(doc.Name);
                Console.WriteLine(doc.TimeToCook);
            }
            return 0;
        }

        static object Index(IndexOptions options)
        {
            RecipeDocument recipe;

            try
            {
                recipe = RecipeFactory.CreateFrom(options.Url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }

            RecipeClient client = SearchClientFactory.CreateClient(options);

            var response = client.Index(recipe);

            Console.WriteLine($"Index: {FormatApiCall(response.ApiCall)}");

            return 0;
        }

        static object Error(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error.ToString());
            }

            return 1;
        }

        static string FormatApiCall(IApiCallDetails details)
        {
            int? status = details.HttpStatusCode;
            bool wasSuccess = details.Success;
            string path = details.Uri.AbsolutePath;

            return $"Response for '{path}', success: {wasSuccess}, status: {status}";
        }
    }
}
