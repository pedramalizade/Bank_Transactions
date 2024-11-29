using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz_Maktab.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Maktab.Configs
{
    public class TransactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
           builder.HasOne(t => t.SourceCard)
                .WithMany(c => c.SentTransaction)
                .HasForeignKey(t => t.SourceCardNumber)
                .OnDelete(DeleteBehavior.Restrict);
            


            builder.HasOne(t => t.DestinationCard)
                .WithMany(c => c.ReceivedTransactions)
                .HasForeignKey(t => t.DestinationCardNumber)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
