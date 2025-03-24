using CardPark.Models;
using Microsoft.EntityFrameworkCore;

namespace CardPark.Repository;

public class CardParkDBContext: DbContext
{
    public CardParkDBContext(DbContextOptions<CardParkDBContext> options) : base(options) { }

}
