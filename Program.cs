using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace DistinctBy {
   public class Size {
      public string Name { get; set; }
      public int Height { get; set; }
      public int Width { get; set; }
   }

   class SizeComparare : IEqualityComparer<Size> {
      private Func<Size, object> _funcDistinct;
      public SizeComparare(Func<Size, object> funcDistinct) {
         this._funcDistinct = funcDistinct;
      }

      public bool Equals(Size x, Size y) {
         return _funcDistinct(x).Equals(_funcDistinct(y));
      }

      public int GetHashCode(Size obj) {
         return this._funcDistinct(obj).GetHashCode();
      }
   }

   class Program {
      static void Main(string[] args) {

         Console.WriteLine("Linq Distinct");
         Size[] adv = { 
                        new Size { Name = "Leaderboard", Height = 90, Width = 728 }, 
                        new Size { Name = "Large Rectangle", Height = 280, Width = 336 }, 
                        new Size { Name = "Large Mobile Banner", Height = 100, Width = 320}, 
                        new Size { Name = "Large Skyscraper", Height = 600, Width = 300 },
                        new Size { Name = "Medium Rectangle", Height = 250, Width = 300},
                        new Size { Name = "Large Skyscraper", Height = 300, Width = 600 },
                      };

         var lst = adv.Distinct();

         foreach (Size p in lst) {
            Console.WriteLine(p.Height + "x" + p.Width + " : " + p.Name);
         }

         Console.WriteLine("\nCompare Distinct");

         var list2 = adv.Distinct(new SizeComparare(a => new { a.Name, a.Height }));
         //var list2 = adv.Distinct(new SizeComparare(a => a.Name));

         foreach (Size p in list2) {
            Console.WriteLine(p.Height + "x" + p.Width + " : " + p.Name);
         }

         Console.WriteLine("\nGroup By way");
         List<Size> list = adv
                               .GroupBy(a => a.Name)
            //.GroupBy(a => new { a.Name, a.Width })
                               .Select(g => g.First())
                               .ToList();

         foreach (Size p in list) {
            Console.WriteLine(p.Height + "x" + p.Width + " : " + p.Name);
         }

         Console.ReadLine();
      }
   }
}