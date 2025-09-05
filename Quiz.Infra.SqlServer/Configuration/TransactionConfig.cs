namespace src.Infrastructure.Db.Quiz.Infra.SqlServer.Configuration
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
