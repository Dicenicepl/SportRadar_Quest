using System;
using System.ComponentModel.DataAnnotations;
using Quest.Models.Results;

namespace Quest.Models.Cards;

public class CardEntity
{
  [Key]
  public int Id { get; set; }

  public int _result_id { get; set; }
  public ResultEntity Result { get; set; } = null!;

  public int Minute { get; set; }
  public string PlayerName { get; set; } = String.Empty;
  public CardTypeEnum CardType { get; set; }
}
public enum CardTypeEnum
{
  YELLOW,
  RED
}