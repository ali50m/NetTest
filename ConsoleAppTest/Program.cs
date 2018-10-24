using IBatisNet.DataMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Db1.ZhiChaoContext())
            {
                var list = db.Cases.Include("Detail").Where(m=>m.Detail!=null).Take(40).ToList();
            }
        }
    }
}
namespace ConsoleAppTest.Db1
{

    class ZhiChaoContext : DbContext
    {
        public ZhiChaoContext() : base(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ZhiChaoGuid;Integrated Security=True;")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Case>().ToTable("Case")
                .HasKey(m => m.Id)
                .HasOptional(m => m.Detail).WithRequired(m => m.Case);
            modelBuilder.Entity<Case>().Property(m => m.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<CaseDetail>().ToTable("CaseDetail")
                .HasKey(m => m.Id)
                .Property(m => m.Id).HasColumnName("CaseId");
        }

        public DbSet<Case> Cases { get; set; }
        public DbSet<CaseDetail> CaseDetails { get; set; }
    }

    class Case
    {
        public Guid Id { get; set; }
        public string CarNum { get; set; }
        public CaseDetail Detail { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", Id, CarNum, Detail?.CarNum2);
        }
    }
    class CaseDetail {
        public Guid Id { get; set; }
        public string CarNum2 { get; set; }
        public Case Case { get; set; }
    }
}

namespace ConsoleAppTest.Db2 { 

    class ZhiChaoContext : DbContext {
        public ZhiChaoContext():base(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ZhiChaoGuid;Integrated Security=True;")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Case>()
                .Map(m=> {
                    m.Properties(a=>new { a.Id,a.CarNum});
                    m.ToTable("Case");
                })
                .Map(m => {
                    m.Property(a => a.Id).HasColumnName("CaseId");
                    m.Properties(a => new { a.Id, a.CarNum2 });
                    m.ToTable("CaseDetail");
                });
        }
        public DbSet<Case> Cases { get; set; }
    }

    class Case {
        public Guid Id { get; set; }
        public string CarNum { get; set; }
        public string CarNum2 { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}",Id,CarNum,CarNum2);
        }
    }
    
}
