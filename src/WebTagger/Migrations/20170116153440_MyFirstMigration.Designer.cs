using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebTagger.Db;

namespace WebTagger.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20170116153440_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("WebTagger.Db.Site", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("WebTagger.Db.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("SiteId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("WebTagger.Db.Tag", b =>
                {
                    b.HasOne("WebTagger.Db.Site", "Site")
                        .WithMany("Tags")
                        .HasForeignKey("SiteId");
                });
        }
    }
}
