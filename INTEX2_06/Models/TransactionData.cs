namespace _INTEX2_06.Models;
public class TransactionData
{
    public int Index { get; set; } = 1;
    public int Time { get; set; } = 1; //this one is looking for decimals.
    public int amount { get; set; } = 25;
    public int country_and_shipping_same { get; set; } = 1; 
    public int day_of_week_Mon { get; set; } = 0;
    public int day_of_week_Sat { get; set; } = 0;
    public int day_of_week_Sun { get; set; } = 0;
    public int day_of_week_Thu { get; set; } = 0;
    public int day_of_week_Tue { get; set; } = 0;
    public int day_of_week_Wed { get; set; } = 0;
    public int entry_mode_PIN { get; set; } = 1;
    public int entry_mode_Tap { get; set; } = 0;
    public int type_of_transaction_Online { get; set; } = 1; //They will always be online.
    public int type_of_transaction_POS { get; set; } = 0;
    public int bank_HSBC { get; set; } = 0;
    public int bank_Halifax { get; set; } = 0;
    public int bank_Lloyds { get; set; } = 0;
    public int bank_Metro { get; set; } = 0;
    public int bank_Monzo { get; set; } = 0;
    public int bank_RBS { get; set; } = 0;
    public int type_of_card_Visa { get; set; } = 0;
}
