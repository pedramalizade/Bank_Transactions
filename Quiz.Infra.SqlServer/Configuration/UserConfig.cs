namespace src.Infrastructure.Db.Quiz.Infra.SqlServer.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           builder.HasKey(x => x.Id);
        }
    }
}
