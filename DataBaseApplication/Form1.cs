using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.TMDb;
using MovieFreak.Models;
using MovieFreak.DAL;
using System.Data.SqlClient;
using MovieFreak.Logic;

namespace DataBaseApplication
{
    public partial class Form1 : Form
    {
        private readonly MovieContext db = new MovieContext();
        
        ServiceClient api = new ServiceClient("cc4b67c52acb514bdf4931f7cedfd12b");

        public Form1()
        {
            InitializeComponent();
        }

        private async Task AddMovies()
        {
            try
            {
                System.Threading.CancellationToken cancellationToken;
                int i = 1; // number of pages
                int j = 0; // number of requests
                int pageCount = 0;
                db.Updates.Add(AddUpdate(DateTime.Now));
                do
                {
                    var movies = await api.Movies.DiscoverAsync(null, false, null, new DateTime(1981, 1, 1), null, 3, 5, null, null, i, cancellationToken);
                    pageCount = movies.PageCount;

                    foreach (var m in movies.Results)
                    {
                        string Director = "";
                        var movie = api.Movies.GetAsync(m.Id, null, true, cancellationToken).Result;
                        var personIdsD = movie.Credits.Crew.Where(s => s.Job == "Director").Select(s => s.Id);

                        //Get Director
                        foreach (var id in personIdsD)
                        {
                            var person = await api.People.GetAsync(id, true, cancellationToken);
                            Director = person.Name;
                        }

                        //Add movie to database
                        j++;
                        Threding(j);
                        var moviesToAdd = db.Movies.SingleOrDefault(mov => mov.MyMovieID == m.Id);
                        if (moviesToAdd == null)
                        {
                            db.Movies.Add(AddMovie(m.Id, m.Title, Director, m.VoteAverage, "https://image.tmdb.org/t/p/w185" + m.Poster, m.ReleaseDate));
                            db.SaveChanges();
                        }

                        //Add Movie generes to database
                        foreach (var g in movie.Genres)
                        {
                            j++;
                            Threding(j);
                            var generes = db.Geners.SingleOrDefault(gen => gen.MyGeneresID == g.Id);
                            if (generes == null)
                            {
                                db.Geners.Add(AddGenere(g.Id, g.Name));
                                db.SaveChanges();  
                            }
                            var genersMovie = db.GenereMovie.Where(mov => mov.MyMovieID == m.Id).SingleOrDefault(gen =>gen.MyGeneresID == g.Id);
                            if(genersMovie == null)
                            {
                                db.GenereMovie.Add(AddGenereMovie(m.Id, g.Id));
                                db.SaveChanges();  
                            }
                        }

                        var personIdsS = movie.Credits.Crew.Where(s => s.Job == "Writer").Select(s => s.Id);

                        //Add Writers to databse
                        foreach (var id in personIdsS)
                        {
                            j++;
                            Threding(j);
                            var person = await api.People.GetAsync(id, true, cancellationToken);
                            var people = db.Writers.SingleOrDefault(wri => wri.MyWritersID == person.Id);
                            if (people == null)
                            {
                                db.Writers.Add(AddWriter(person.Id, person.Name));
                                db.WriterMovie.Add(AddWritersMovie(m.Id, person.Id));
                                db.SaveChanges();
                            }
                            var writersMovie = db.WriterMovie.Where(mov => mov.MyMovieID == m.Id).SingleOrDefault(wri => wri.MyWritersID == person.Id);
                            if(writersMovie == null)
                            {
                                db.WriterMovie.Add(AddWritersMovie(m.Id, person.Id));
                                db.SaveChanges();
                            }                          
                        }

                        //Add Actors to database fixed to 6
                        var cast = await api.Movies.GetCreditsAsync(m.Id, cancellationToken);
                        List<MediaCredit> ActorsList = cast.ToList();
                        List<MediaCredit> ActorsListFixed = new List<MediaCredit>();
                        int listElements = ActorsList.Count();
                        if (listElements > 6)
                        {
                            ActorsListFixed = ActorsList.GetRange(0, 6);
                        }
                        else
                        {
                            ActorsListFixed = ActorsList.GetRange(0, listElements);
                        }

                        foreach (var a in ActorsListFixed)
                        {
                            j++;
                            Threding(j);
                            var actors = db.Actors.SingleOrDefault(act => act.MyActorID == a.Id);
                            if (actors == null)
                            {
                                db.Actors.Add(AddActor(a.Id, a.Name));
                                db.MovieActor.Add(AddMovieActor(m.Id, a.Id));
                                db.SaveChanges();
                            }
                            var actrosMovie = db.MovieActor.Where(mov => mov.MyMovieID == m.Id).SingleOrDefault(act => act.MyActorID == a.Id); 
                            if(actrosMovie == null)
                            {
                                db.MovieActor.Add(AddMovieActor(m.Id, a.Id));
                                db.SaveChanges();
                            }
                        }
                    }
                    i++;
                } while (i <= pageCount);                
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void Threding(int j)
        {
            if (j % 40 == 0)
            {
                Thread.Sleep(10000);
                j = 0;
            }
        }

        private DateOfUpdate AddUpdate(DateTime data)
        {
            DateOfUpdate newUpdate = new DateOfUpdate();
            newUpdate.UpdateDate = data;
            return newUpdate;
        }

        private MyMovie AddMovie(int id, string title, string director, decimal rating, string url, DateTime? date)
        {
            MyMovie Movie = new MyMovie();
            Movie.MyMovieID = id;
            Movie.Title = title;
            Movie.Director = director;
            Movie.Rating = rating;
            Movie.MovieArtUrl = url;
            Movie.ReleaseDate = date;
            //Movie.Description = description; 
            return Movie;
        }

        private MyActor AddActor(int id, string name)
        {
            MyActor Actor = new MyActor();
            Actor.MyActorID = id;
            Actor.ActorName = name;
            return Actor;
        }

        private MyWriters AddWriter(int id, string name)
        {
            MyWriters Writer = new MyWriters();
            Writer.MyWritersID = id;
            Writer.WriterName = name;
            return Writer;
        }

        private MyGeneres AddGenere(int id, string name)
        {
            MyGeneres Genere = new MyGeneres();
            Genere.MyGeneresID = id;
            Genere.GenereName = name;
            return Genere;
        }

        private MyGeneresMyMovie AddGenereMovie(int movieID, int genereID)
        {
            MyGeneresMyMovie genereMovie = new MyGeneresMyMovie();
            genereMovie.MyMovieID = movieID;
            genereMovie.MyGeneresID = genereID;
            return genereMovie;
        }

        private MyWritersMyMovie AddWritersMovie(int movieID, int writeresID)
        {
            MyWritersMyMovie writersMovie = new MyWritersMyMovie();
            writersMovie.MyMovieID = movieID;
            writersMovie.MyWritersID = writeresID;
            return writersMovie;
        }

        private MyMovieMyActor AddMovieActor(int movieID, int actorID)
        {
            MyMovieMyActor movieActor = new MyMovieMyActor();
            movieActor.MyMovieID = movieID;
            movieActor.MyActorID = actorID;
            return movieActor;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            label2.Text = db.Movies.Count().ToString();
            label3.Text = db.Rules.Count().ToString();
            label8.Text = "0";
            label9.Text = "0";
            if (db.Updates.Count() > 0)
            {
                label6.Text = db.Updates.ToList().LastOrDefault().UpdateDate.ToString("d");
            }
            else
            {
                label6.Text = "";
            }
            PopulateListView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddMovies();
            label2.Text = db.Movies.Count().ToString();
            if (db.Updates.Count() > 0)
            {
                label6.Text = db.Updates.LastOrDefault().UpdateDate.ToString();
            }
            else
            {
                label6.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int timeStart, timeStop, timeElapsed;
            timeStart = Environment.TickCount;
            AddRules();
            timeStop = Environment.TickCount;
            timeElapsed = -(timeStart - timeStop) / 1000;

            label8.Text = timeElapsed.ToString();
            label3.Text = db.Rules.Count().ToString();
            PopulateListView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int timeStart, timeStop, timeElapsed;
            timeStart = Environment.TickCount;
            AddRules2();
            timeStop = Environment.TickCount;
            timeElapsed = -(timeStart - timeStop) / 1000;
            
            label9.Text = timeElapsed.ToString();
            label3.Text = db.Rules.Count().ToString();
            PopulateListView();
        }

        private void AddRules()
        {
            try
            {
                var filmList = db.Movies.ToList();
                Itemset myItemset = new Itemset();
                foreach (var i in filmList)
                {
                    myItemset.Add(i.MyMovieID);
                }

                ItemsetCollection myItemsetCollection = new ItemsetCollection();
                var MovieList = db.UserMovieLists.Select(u => u);
                foreach (var u in MovieList)
                {
                    var query = from a in db.Ratings
                                join b in db.UserMovieLists
                                    on a.UserMovieLIstID equals b.UserMovieListID
                                    into temp
                                from b in temp.DefaultIfEmpty()
                                where a.UserMovieLIstID == u.UserMovieListID
                                where a.UserRatingID >= 5
                                select new
                                {
                                    a.MyMovieID,
                                };
                    Itemset tempItemset = new Itemset();
                    ;
                    if (myItemset != null)
                    {
                        if (query.Count() > 1)
                        {
                            foreach (var r in query.Take(25))
                            {
                                tempItemset.Add(r.MyMovieID);
                            }
                            myItemsetCollection.Add(tempItemset);
                        }
                    }
                }

                ItemsetCollection L = AprioriMining.Apriori(myItemsetCollection, 20);
                List<AssociationRule> allRules = AprioriMining.Mine(myItemsetCollection, L, 20);
                
                var ruleX = db.RulesX.Select(x => x);
                db.RulesX.RemoveRange(ruleX);
                db.SaveChanges();
                var ruleY = db.RulesY.Select(x => x);
                db.RulesY.RemoveRange(ruleY);
                db.SaveChanges();
                var myRule = db.Rules.Select(x => x);
                db.Rules.RemoveRange(myRule);
                db.SaveChanges();
                var ruleXmovie = db.RuleXMovie.Select(x => x);
                db.RuleXMovie.RemoveRange(ruleXmovie);
                db.SaveChanges();
                var ruleYMovie = db.RuleYMovie.Select(x => x);
                db.RuleYMovie.RemoveRange(ruleYMovie);

                foreach(var r in allRules)
                {
                    var rule = new MyRule();
                    var X = new RuleX();
                    var Y = new RuleY();
                    db.RulesX.Add(X);
                    db.SaveChanges();
                    db.RulesY.Add(Y);
                    db.SaveChanges();
                    rule.Support = r.Support;
                    rule.Confidene = r.Confidence;
                    rule.RuleXID = X.RuleXID;
                    rule.RuleYID = Y.RuleYID;
                    db.Rules.Add(rule);
                    db.SaveChanges();

                    foreach(var x in r.X)
                    {
                        var rm = new RuleXMyMovie();
                        rm.RuleX_RuleXID = X.RuleXID;
                        rm.MyMovie_MyMovieID = x;
                        db.RuleXMovie.Add(rm);
                        db.SaveChanges();
                    }
                    foreach(var y in r.Y)
                    {
                        var rm = new RuleYMyMovie();
                        rm.RuleY_RuleYID = Y.RuleYID;
                        rm.MyMovie_MyMovieID = y;
                        db.RuleYMovie.Add(rm);
                        db.SaveChanges();
                    }
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void AddRules2()
        {
            try
            {
                var filmList = db.Movies.ToList();
                Itemset myItemset = new Itemset();
                foreach (var i in filmList)
                {
                    myItemset.Add(i.MyMovieID);
                }

                ItemsetCollection myItemsetCollection = new ItemsetCollection();
                var MovieList = db.UserMovieLists.Select(u => u);
                foreach (var u in MovieList)
                {
                    var query = from a in db.Ratings
                                join b in db.UserMovieLists
                                    on a.UserMovieLIstID equals b.UserMovieListID
                                    into temp
                                from b in temp.DefaultIfEmpty()
                                where a.UserMovieLIstID == u.UserMovieListID
                                where a.UserRatingID >= 5
                                select new
                                {
                                    a.MyMovieID,
                                };
                    Itemset tempItemset = new Itemset();
                    ;
                    if (myItemset != null)
                    {
                        if (query.Count() > 1)
                        {
                            foreach (var r in query.Take(25))
                            {
                                tempItemset.Add(r.MyMovieID);
                            }
                            myItemsetCollection.Add(tempItemset);
                        }
                    }
                }

                ItemsetCollection L = AprioriMining.AprioriTid(myItemsetCollection, 20);
                List<AssociationRule> allRules = AprioriMining.Mine(myItemsetCollection, L, 20);

                var ruleX = db.RulesX.Select(x => x);
                db.RulesX.RemoveRange(ruleX);
                db.SaveChanges();
                var ruleY = db.RulesY.Select(x => x);
                db.RulesY.RemoveRange(ruleY);
                db.SaveChanges();
                var myRule = db.Rules.Select(x => x);
                db.Rules.RemoveRange(myRule);
                db.SaveChanges();
                var ruleXmovie = db.RuleXMovie.Select(x => x);
                db.RuleXMovie.RemoveRange(ruleXmovie);
                db.SaveChanges();
                var ruleYMovie = db.RuleYMovie.Select(x => x);
                db.RuleYMovie.RemoveRange(ruleYMovie);

                foreach (var r in allRules)
                {
                    var rule = new MyRule();
                    var X = new RuleX();
                    var Y = new RuleY();
                    db.RulesX.Add(X);
                    db.SaveChanges();
                    db.RulesY.Add(Y);
                    db.SaveChanges();
                    rule.Support = r.Support;
                    rule.Confidene = r.Confidence;
                    rule.RuleXID = X.RuleXID;
                    rule.RuleYID = Y.RuleYID;
                    db.Rules.Add(rule);
                    db.SaveChanges();

                    foreach (var x in r.X)
                    {
                        var rm = new RuleXMyMovie();
                        rm.RuleX_RuleXID = X.RuleXID;
                        rm.MyMovie_MyMovieID = x;
                        db.RuleXMovie.Add(rm);
                        db.SaveChanges();
                    }
                    foreach (var y in r.Y)
                    {
                        var rm = new RuleYMyMovie();
                        rm.RuleY_RuleYID = Y.RuleYID;
                        rm.MyMovie_MyMovieID = y;
                        db.RuleYMovie.Add(rm);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void PopulateListView()
        {
            listView1.Items.Clear();
            var result = from rules in db.Rules
                         join ruleX in db.RulesX on rules.RuleXID equals ruleX.RuleXID
                         join ruleY in db.RulesY on rules.RuleYID equals ruleY.RuleYID
                         join xMovie in db.RuleXMovie on ruleX.RuleXID equals xMovie.RuleX_RuleXID
                         join yMovie in db.RuleYMovie on ruleY.RuleYID equals yMovie.RuleY_RuleYID
                         select new { id1 = ruleX.RuleXID, id2 = ruleY.RuleYID, support = rules.Support, confidence = rules.Confidene };

            var MoviesRulesXList = new List<int>();
            var MoviesRulesYList = new List<int>();
            List<string[]> ListTables = new List<string[]>();
            List<string> joinstring = new List<string>();
            var ListItem = new ListViewItem();
            int i = 1;

            foreach(var r in result)
            {
                string ele1 = (i).ToString();
                MoviesRulesXList = db.RuleXMovie.Where(x => x.RuleX_RuleXID == r.id1).Select(x => x.MyMovie_MyMovieID).ToList();
                string movies = "";
                foreach(var ele in MoviesRulesXList)
                {
                    if (ele != null)
                    {
                        var movie = db.Movies.Find(ele);
                        movies = movies + movie.Title + ", ";
                    }
                }
                string ele2 = movies;
                string ele3 = "==>";
                MoviesRulesYList = db.RuleYMovie.Where(x => x.RuleY_RuleYID == r.id2).Select(x => x.MyMovie_MyMovieID).ToList();
                string movies2 = "";
                foreach (var ele in MoviesRulesYList)
                {
                    if (ele != null)
                    {
                        var movie = db.Movies.Find(ele);
                        movies2 = movies2 + movie.Title + ", ";
                    }
                }
                string ele4 = movies2;
                string ele5 = r.support.ToString();
                string ele6 = r.confidence.ToString();

                string compare = ele2 + ele3 + ele4 + ele5 + ele6;                
                if(!joinstring.Contains(compare))
                {
                    joinstring.Add(compare);
                    ListTables.Add(new string[] {ele1, ele2, ele3, ele4, ele5, ele6 });
                    i++;
                }
            }

            foreach(var tab in ListTables)
            {
                ListItem = new ListViewItem(tab);
                listView1.Items.Add(ListItem);
            }
        }

        private void PopulateChart()
        {
            try
            {
                var filmList = db.Movies.ToList();
                Itemset myItemset = new Itemset();
                foreach (var i in filmList)
                {
                    myItemset.Add(i.MyMovieID);
                }

                ItemsetCollection myItemsetCollection = new ItemsetCollection();
                var MovieList = db.UserMovieLists.Select(u => u);
                foreach (var u in MovieList)
                {
                    var query = from a in db.Ratings
                                join b in db.UserMovieLists
                                    on a.UserMovieLIstID equals b.UserMovieListID
                                    into temp
                                from b in temp.DefaultIfEmpty()
                                where a.UserMovieLIstID == u.UserMovieListID
                                where a.UserRatingID >= 5
                                select new
                                {
                                    a.MyMovieID,
                                };
                    Itemset tempItemset = new Itemset();
                    ;
                    if (myItemset != null)
                    {
                        if (query.Count() > 1)
                        {
                            foreach (var r in query.Take(25))
                            {
                                tempItemset.Add(r.MyMovieID);
                            }
                            myItemsetCollection.Add(tempItemset);
                        }
                    }
                }

                List<double> supportList = new List<double>();
                double support1 = 30;
                double support2 = 27.5;
                double support3 = 25;
                double support4 = 22.5;
                double support5 = 20;
                supportList.Add(support5);
                supportList.Add(support4);
                supportList.Add(support3);
                supportList.Add(support2);
                supportList.Add(support1);

                foreach (var ele in supportList)
                {
                    int timeStart, timeStop, timeElapsed;
                    timeStart = Environment.TickCount;
                    ItemsetCollection L = AprioriMining.Apriori(myItemsetCollection, ele);
                    timeStop = Environment.TickCount;
                    timeElapsed = (timeStop - timeStart) / 1000;
                    this.chart1.Series["Apriori"].Points.AddXY(ele, timeElapsed);
                    
                }

                foreach (var ele in supportList)
                {
                    int timeStart, timeStop, timeElapsed;
                    timeStart = Environment.TickCount;
                    ItemsetCollection L = AprioriMining.AprioriTid(myItemsetCollection, ele);
                    timeStop = Environment.TickCount;
                    timeElapsed = (timeStop - timeStart) / 1000;
                    this.chart1.Series["AprioriTid"].Points.AddXY(ele, timeElapsed);
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PopulateChart();
        }
    }
}
