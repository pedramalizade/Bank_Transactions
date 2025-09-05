namespace src.Infrastructure.Db.Quiz.Infra.SqlServer.Configuration
{
    public class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.CardNumber);

            builder.HasOne(a => a.User)
                .WithMany(x => x.Cards)
                .HasForeignKey(x => x.UserId);
        }
    }
}
