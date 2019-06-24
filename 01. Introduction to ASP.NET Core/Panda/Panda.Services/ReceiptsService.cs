﻿//namespace Panda.Services
//{
//    using Panda.Domain.Models;
//    using Panda.Persistence;
//    using Panda.Services.Interfaces;
//    using System;
//    using System.Linq;
    
//    public class ReceiptsService : IReceiptsService
//    {
//        private readonly PandaDbContext db;

//        public ReceiptsService(PandaDbContext db)
//        {
//            this.db = db;
//        }

//        public void CreateFromPackage(decimal weight, string packageId, string recipientId)
//        {
//            var receipt = new Receipt
//            {
//                PackageId = packageId,
//                RecipientId = recipientId,
//                Fee = weight * 2.67M,
//                IssuedOn = DateTime.UtcNow,
//            };

//            this.db.Receipts.Add(receipt);
//            this.db.SaveChanges();
//        }

//        public IQueryable<Receipt> GetAll()
//        {
//            return this.db.Receipts;
//        }
//    }
//}