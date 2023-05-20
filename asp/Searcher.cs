using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using lucene = Lucene.Net;
using docs = Lucene.Net.Documents;
using Lucene.Net.Analysis;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using Lucene.Net.Search;
using static Lucene.Net.Documents.Field;


class LuceneContext
{
    public const lucene.Util.Version ver = lucene.Util.Version.LUCENE_30;

    public static readonly lucene::Analysis.Analyzer analyzer =
        new lucene.Analysis.El.GreekAnalyzer(ver);
};

namespace Searcher
{
    public class Srch
    {
        private static lucene.Store.Directory dir = lucene.Store.FSDirectory.Open(
               "./index");

        private static (ImmutableArray<lucene.Search.ScoreDoc>, long, lucene.Search.IndexSearcher) __search(
       lucene.Index.IndexReader ir, string query, long Dfrom, long Dto,
       double from, double to, int numofresults = 20, int offset = 0)
        {
            lucene.Search.IndexSearcher s = new(ir);
            var f1 = lucene.Search.NumericRangeQuery.NewLongRange("Date", Dfrom, Dto, false, false);
            var f2 = lucene.Search.NumericRangeQuery.NewDoubleRange("Price", from, to, false, false);
            var bq = new lucene.Search.BooleanQuery();
            if (query != string.Empty && query is not null) {
                var q = new lucene.QueryParsers
                    .QueryParser(LuceneContext.ver, "Name", LuceneContext.analyzer)
                    .Parse(query);
                bq.Add(q, 0);
            }
            bq.Add(f1, 0);
            bq.Add(f2, 0);
            var scoredocs = s
                .Search(bq,int.MaxValue )
                .ScoreDocs;
            long res = scoredocs.Length;
            return (scoredocs
                .Skip(offset* numofresults)
                .Take(numofresults)
                .ToImmutableArray(), res, s);
        }


        private static ResponseJson __prepForserialization(ImmutableArray<lucene.Search.ScoreDoc> docs, long resct,
           IndexSearcher src)
        {
            ResponseJson r = new();
            r.NumberOfResults = resct;
            r.Data = new ResponseData[docs.Length];
            for (int i = 0; i < docs.Length; ++i)
            {
                var doc = src.Doc(docs[i].Doc);
                ResponseData hit = new();
                hit.moduleID = long.Parse(doc.Get("Id"));
                hit.Date = new DateTime( long.Parse( doc.Get("Date") ) ).ToString();
                hit.Price = double.Parse(doc.Get("Price"));
                hit.Rating = float.Parse(doc.Get("Rating"));
                hit.Header = doc.Get("Name");
                hit.ProfessorName = doc.Get("Professor");
                r.Data[i] = hit;
            }
            return r;
        }

        public static ResponseJson search(Query q)
        {
            using var reader = lucene.Index.IndexReader.Open(dir, true);
            var (docs, num, src) = __search(reader,
                q.query, DateTime.Parse(q.dateRange.from).Ticks,
                DateTime.Parse(q.dateRange.to).Ticks,
                q.priceRange.low, q.priceRange.high,
                q.numOfResults, q.offset);
            return __prepForserialization(docs, num, src); 
        }
    }

    public class priceRange
    {
        public double low { get; set; } 
        public double high { get; set; }
        public override string ToString()
        {
            return low + " " + high;
        }

    }
    public class dateRange
    {
        public string from { get; set; }
        public string to { get; set; }

        public override string ToString()
        {
            return from +" " + to;
        }

    }

    public class Query
    {
        public string? query { get; set; }
        public priceRange priceRange { get; set; }
        public dateRange dateRange { get; set; }
        public int offset { get; set; }

        public int numOfResults { get; set; }

        public override string ToString()
        {
            return query + " " +priceRange.ToString()+" "+dateRange.ToString()
                +" "+offset;
        }


    }

    public class ResponseJson
    {
        public ResponseData[] Data { get; set; }
        public long NumberOfResults { get; set; }
    }
    public class ResponseData
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public string ProfessorName { get; set; }
        public double Price { get; set; }
        public string Date{ get; set; }
        public long moduleID { get; set; }
        public float Rating { get; set; }

    }

}

