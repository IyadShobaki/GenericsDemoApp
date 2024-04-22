using System;
using System.Collections.Generic;

namespace GenericsDemo
{
   class Program
   {
      static void Main(string[] args)
      {
         //List<string> stringList = new List<string>();
         //List<int> intList = new List<int>();

         //intList.Add(1);
         //string result = "";

         //result = FizzBuzz("tests");

         //Console.WriteLine($"Tests: { result }");

         //result = FizzBuzz(123);

         //Console.WriteLine($"123: { result }");

         // result = FizzBuzz(true);

         // Console.WriteLine($"true: {result}");

         //result = FizzBuzz(new PersonModel { FirstName = "Tim", LastName = "Corey" });

         //Console.WriteLine($"PersonModel: { result }");



         GenericHelper<PersonModel> peopleHelper = new GenericHelper<PersonModel>();
         peopleHelper.CheckItemAndAdd(new PersonModel { FirstName = "Tim", HasError = true });

         foreach (var item in peopleHelper.RejectedItems)
         {
            Console.WriteLine($"{item.FirstName} {item.LastName} was rejected.");
         }

         Console.ReadLine();
      }

      // 3 - Fizz, 5 - Buzz, 3 & 5 - FizzBuzz
      private static string FizzBuzz<T>(T item)
      {
         int itemLength = item.ToString().Length;
         Console.WriteLine($"itemLength: {itemLength}");
         Console.WriteLine($"item: {item}");
         string output = "";

         if (itemLength % 3 == 0)
         {
            output += "Fizz";
         }

         if (itemLength % 5 == 0)
         {
            output += "Buzz";
         }

         return output;
      }
   }

   // when you pass type T at the class level, the whole class can use type T. (for example you don't have to add <T> 
   // in front of the method becuase they know about it - public void CheckItemAndAdd'<T>'(T item)


   // You can add class to make T has to be a class
   // public class GenericHelper<T> where T : class, IErrorCheck
   // But becuase T implement interface that indicated that its a class
   // Only classes can implement interfaces
   // you can add new() - GenericHelper<T> where T : IErrorChec, new() - to indicate that the class has to have 
   // an empty constructor. becuase not every class has empty constructor
   // and we can use new() if we need to create an instance of the class
   // To limit you can use PersonModel to limit T as Person class or its children
   // To limit you can use "struct" which make T is a value type only (int, double, bool, etc.) // string is not a struct
   // You can limit two generics like this ( GenericHelper<T, U> where T : IErrorCheck
   // where U: class  )
   public class GenericHelper<T> where T : IErrorCheck // T has to implement IErrorCheck interface

   {
      public List<T> Items { get; set; } = new List<T>();
      public List<T> RejectedItems { get; set; } = new List<T>();

      public void CheckItemAndAdd(T item) // You can pass IErrorCheck interface but
                                          // it has to convert to Person or Car, etc. model and its not efficient
                                          // and you don't have access to the specific model properties or methods
                                          // So we put it at the class level - T has to implement IErrorCheck interface
      {
         if (item.HasError == false)
         {
            Items.Add(item);
         }
         else
         {
            RejectedItems.Add(item);
         }
      }
   }

   public interface IErrorCheck
   {
      bool HasError { get; set; }
   }

   public class CarModel : IErrorCheck
   {
      public string Manufacturer { get; set; }
      public int YearManufactured { get; set; }
      public bool HasError { get; set; }
   }

   public class PersonModel : IErrorCheck
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public bool HasError { get; set; }
   }
}
