// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using LAB17.Classes;
using LAB17;

using (AutoContext db = new AutoContext())
{
    // t1
    var res1 = db.Clients.Where(o => o.Discount > 5).Select(o => new { o.ClientFirstName, o.ClientLastName }).ToList();
    foreach (var item in res1)
    {
        Console.WriteLine($"{item.ClientFirstName} {item.ClientLastName}");
    }
    // t2
    var res2 = db.Clients.Where(o => o.Discount.Equals(null)).Select(o => new { o.ClientFirstName, o.ClientPhoneNumber }).ToList();
    foreach (var item in res2)
    {
        Console.WriteLine($"{item.ClientFirstName} {item.ClientPhoneNumber}");
    }
    // t3
    var res3 = db.Autos.Select(o => new { o.Mark }).Distinct().ToList();
    foreach (var item in res3)
    {
        Console.WriteLine($"{item.Mark}");
    }
    // t4
    var res4 = db.Autos.Where(o => (DateTime.Today.Year - o.Year) > 5).Select(o => new { o.Mark, o.Model, o.Color, o.Year });
    foreach (var item in res4)
    {
        Console.WriteLine($"{item.Mark}-{item.Model}-{item.Year}-{item.Color}");
    }
    // t5
    var res5 = db.Autos.Where(o => o.Mark.Equals("Ford")).Select(o => new { o.RegNumber, o.Model });
    foreach (var item in res5)
    {
        Console.WriteLine($"{item.RegNumber}-{item.Model}");
    }
    // t6
    var res6 = db.Servises.Where(o => o.Duration > 15 && o.Duration <= 45).Select(o => new { o.Title, o.Price });
    foreach (var item in res6)
    {
        Console.WriteLine($"{item.Title}-{item.Price}");
    }
    // t7
    var res7 = db.Autos.Where(o =>
    o.Mark.Equals("Toyota") || o.Mark.Equals("Mazda") || o.Mark.Equals("Jaguar")).Select(o => new { o.Model }).Distinct().ToList();
    foreach (var item in res7)
    {
        Console.WriteLine($"{item.Model}");
    }
    // t8
    var res8 = db.Workers.Select(o => o.WorkerLastName).Distinct().ToList();
    foreach (var item in res8)
    {
        Console.WriteLine($"{item}");
    }
    // t9
    var res9 = db.Workers.Where(o => o.WorkerEndEmpDate != null).Select(o => new { o.WorkerFirstName, o.WorkerLastName, o.WorkerPhoneNumber, o.WorkerStartEmpDate, o.WorkerEndEmpDate });
    foreach (var item in res9)
    {
        Console.WriteLine($"{item.WorkerFirstName}-{item.WorkerLastName}-{item.WorkerPhoneNumber}-{item.WorkerStartEmpDate}-{item.WorkerEndEmpDate}");
    }
    // t10
    var res10 = db.Autos.Join(db.Clients, o => o.ClientsId, x => x.Id,
        (o, x) => new { o.RegNumber, o.Mark, o.Model, o.Year, o.Color, x.ClientFirstName, x.ClientLastName, x.ClientPhoneNumber, x.Discount });
    foreach (var item in res10)
    {
        Console.WriteLine($"{item.RegNumber}-{item.Mark}-{item.Model}-{item.Year}-{item.Color} --" +
            $"-- {item.ClientFirstName}-{item.ClientLastName}-{item.ClientPhoneNumber}-{item.Discount}");
    }
    // t11
    var res11 = db.Orders.Join(db.Workers, o => o.WorkersId, x => x.Id,
        (o, x) => new { o.ServisesId, o.AutosId, x.WorkerLastName, x.WorkerFirstName })
        .Join(db.Servises, o => o.ServisesId, x => x.Id,
        (o, x) => new { o.WorkerFirstName, o.WorkerLastName, o.AutosId, x.Title })
        .Join(db.Autos, o => o.AutosId, x => x.Id,
        (o, x) => new { o.WorkerFirstName, o.WorkerLastName, o.Title, x.RegNumber, x.ClientsId })
        .Join(db.Clients, o => o.ClientsId, x => x.Id,
        (o, x) => new { o.WorkerFirstName, o.WorkerLastName, o.Title, o.RegNumber, x.ClientLastName, x.ClientFirstName });
    foreach (var item in res11)
    {
        Console.WriteLine($"{item.Title}-{item.WorkerFirstName} {item.WorkerLastName}-{item.RegNumber}-{item.ClientFirstName} -" +
            $"{item.ClientLastName}");
    }
    // t12
    var res12 = db.Orders.Where(x => x.Ended != null).Select(o => o.Id).ToList();
    foreach (var item in res12)
    {
        Console.WriteLine($"{item}");
    }
    // t13
    var res13 = db.Orders.Where(o => o.Ended == null)
        .Join(db.Workers, o => o.WorkersId, x => x.Id,
        (o, x) => new { o.ServisesId, o.AutosId, x.WorkerLastName, x.WorkerFirstName, o.Created })
        .Join(db.Servises, o => o.ServisesId, x => x.Id,
        (o, x) => new { o.WorkerFirstName, o.WorkerLastName, o.AutosId, x.Title, x.Price })
        .Where(x => x.Price > 500)
        .Join(db.Autos, o => o.AutosId, x => x.Id,
        (o, x) => new { o.WorkerFirstName, o.WorkerLastName, o.Title, x.RegNumber, x.ClientsId })
        .Join(db.Clients, o => o.ClientsId, x => x.Id,
        (o, x) => new { o.WorkerFirstName, o.WorkerLastName, o.Title, o.RegNumber, x.ClientLastName, x.ClientFirstName });
    foreach (var item in res13)
    {
        Console.WriteLine($"{item.Title}-{item.WorkerFirstName} {item.WorkerLastName}-{item.RegNumber}-{item.ClientFirstName} -" +
           $"{item.ClientLastName}");
    }
    // t14
    var res14 = db.Orders.Where(o => o.Ended != null)
        .Join(db.Workers, o => o.WorkersId, x => x.Id,
        (o, x) => new { o.ServisesId, o.AutosId, x.WorkerLastName, x.WorkerFirstName })
        .Join(db.Servises, o => o.ServisesId, x => x.Id,
        (o, x) => new { o.WorkerFirstName, o.WorkerLastName, o.AutosId, x.Title, x.Price })
        .Join(db.Autos, o => o.AutosId, x => x.Id,
        (o, x) => new { o.WorkerFirstName, o.WorkerLastName, o.Title, x.RegNumber, x.ClientsId })
        .Join(db.Clients, o => o.ClientsId, x => x.Id,
        (o, x) => new { o.WorkerFirstName, o.WorkerLastName, o.Title, o.RegNumber, x.ClientLastName, x.ClientFirstName })
        .Where(o => o.ClientFirstName.Equals("Иван") && o.ClientLastName.Equals("Петров"));
    foreach (var item in res14)
    {
        Console.WriteLine($"{item.Title}-{item.WorkerFirstName} {item.WorkerLastName}-{item.RegNumber}-{item.ClientFirstName} -" +
           $"{item.ClientLastName}");
    }
    // t15
    var res15 = db.Servises.Select(x => new { x.Title, x.Duration, x.Price }).OrderBy(x => x.Price);
    foreach (var item in res15)
    {
        Console.WriteLine($"{item.Title}-{item.Duration}-{item.Price}");
    }
}
