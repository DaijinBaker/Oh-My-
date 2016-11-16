//Allows access to the classes of these systems, to be used in the script
using SQLite4Unity3d;

public class ItemDTO  { //creating a public class of the table ItemDTO

	[PrimaryKey, AutoIncrement] //setting primary keys and auto increment
	public string ItemID { get; set; } //
	public string ItemDescription { get; set; }

	public override string ToString () //creating a public overide string called ToString
	{
		return string.Format ("[Item: ItemID={0},  ItemDescription={1}]", ItemID, ItemDescription); //returning the string in format
	}
}
