// See https://aka.ms/new-console-template for more information

using lucene = Lucene.Net;


using docs = Lucene.Net.Documents;
using Lucene.Net.Analysis;
using System.Collections.Immutable;
using csv = CsvHelper;

using NumSharp;

using np = NumSharp.np;





class Program
{
    


    static IndexBuilder ib = new IndexBuilder();

    static docs.IFieldable[] MakeFields(long id , string prof ,  string content , long DateToLong , double price , double rating)
    {
        var storedId = new docs.Field("Id", id.ToString(), docs.Field.Store.YES,
            docs.Field.Index.NOT_ANALYZED);

        var storedProf = new docs.Field("Professor", prof, docs.Field.Store.YES,
           docs.Field.Index.NOT_ANALYZED);

        var moduleName = new docs.Field("Name", content,
            docs.Field.Store.YES, docs.Field.Index.ANALYZED);

        var DateField  = new docs.NumericField("Date", docs.Field.Store.YES, true);
        DateField.SetLongValue(DateToLong);

        var PriceField = new docs.NumericField("Price", docs.Field.Store.YES, true);
        PriceField.SetDoubleValue(price);

        var RateField = new docs.NumericField("Rating", docs.Field.Store.YES, true);
        RateField.SetDoubleValue(rating);

        return new docs.IFieldable[] { storedId, storedProf, moduleName, DateField, PriceField, RateField };
    }

   

    const string path = "./shot.csv";

    public class DbCols
    {
        public int id { get; set; }
        public string name { get; set; }
        public string date {get;set;}
        public int hours { get; set; }
        public string prof { get; set; }
        public int price { get; set; }
        public float rating { get; set; }

        public override string ToString()
        {
            return "[ "+id + "  " + name + "  " + date
                +"  "+hours+"  "+prof+"  "+price+"  "
                +rating+" ]";
        }
    }
    public static void Main(params string[] args)
    {


        var reader = new StreamReader(path);
        using(var CSV = new csv.CsvReader(reader , System.Globalization.CultureInfo.InvariantCulture))
        {
            var records = CSV.GetRecords<DbCols>().ToArray();
            lucene.Store.Directory dir = lucene.Store.FSDirectory.Open(
          "./index");
            var ir = new lucene.Index.IndexWriter(dir, LuceneContext.analyzer,
                lucene.Index.IndexWriter.MaxFieldLength.UNLIMITED);

            foreach(var record in records)
            {
                long date = DateTime.Parse(record.date).Ticks;
                ib.AddDoc(ir, MakeFields(record.id, record.prof, record.name, date, record.price,
                    record.rating));
                
            }
            ir.Commit();
         
        }

    }

}
