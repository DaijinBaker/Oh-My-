//Allows access to the classes of these systems, to be used in the script
using SQLite4Unity3d;

public class Account  {

	[PrimaryKey, AutoIncrement]
	public string Username { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Person: Username={0}, Email={1},  Password={2}]", Username, Email, Password);
	}
}
