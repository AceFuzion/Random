/* Stephanie Hunt
    10/30/22
    A program to read a .txt file of names. Then sort those names alphabetically
    by last name, then by first name. Lastly it writes the final sorted list to
    a new file.
*/
using System;
using System.IO;
using System.Collections.Generic;

public class Sort
{
    class Person
	{
    	public Person(string firstName, string lastName)
    	{
        	FirstName = firstName;
        	LastName = lastName;
    	}
    	public string FirstName {get;set;}
    	public string LastName {get;set;}
	}

	class PersonComparer: IComparer<Person>
	{
    	public int Compare(Person a, Person b)
    	{
        	var lastName = a.LastName.CompareTo(b.LastName);
        	if(lastName == 0)
            	return a.FirstName.CompareTo(b.FirstName);
        	return lastName;
    	}   
	}
    
    public static void Main(string[] args)
    {
        // Create a list to hold all the names and a Person list
		// to manipulate for last name sort
		List<string> list = new List<string>();
		List<Person> personList = new List<Person>();
		//Person[] personList;
		int count = 0;
           
        try
        {
		    string line;
            // Read names from .txt file & put them into array
            using (StreamReader reader = new StreamReader("input.txt"))
            while((line = reader.ReadLine()) != null)
            {
				// Splits the strings up based off the the new line
				// then inserts them into the list
                var parts = line.Split('\n');
           		list.Add(string.Join(" ", parts));
				count++;
            }
        }
        catch(FileNotFoundException e)
        {
            Console.WriteLine("Error opening file. Data not loaded");
		}
		
		// Convert the list into an array
		string[] nameArray = list.ToArray();
		
		// Split the names from the names array into first and last names
		// to input into the Person class
		for(int i = 0; i < count; i++)
		{
			string name = nameArray[i];
			var last = name.Substring(' ' + 1).ToString();
			var first = name.Split(' ').ToString();
			Person p = new Person(first, last);
			personList.Add(p);
		}
        
		// Sort Person list by last name
		personList.Sort(new PersonComparer());
		
        // Sort the list in alphabetical order by first name
        Array.Sort(nameArray);
		
		// Print the sorted array
		foreach(var name in nameArray)
		{
    		Console.WriteLine(name.ToString());
		}
		
		// Convert the newly sorted array back into a list
		List<string> sortedList = new List<string>(nameArray);
		
		// Write the list to a new file
        using (StreamWriter writer = new StreamWriter("output.txt"))
        {
            foreach(var line in sortedList)
            writer.WriteLine(line);
        }
    } // end main
}
