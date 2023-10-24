using System.IO;
class Filer
{
    readonly string id_Path;
    readonly string path;
    int id = 0;

    public Filer(string _path)
    {
        id_Path = _path + "id.txt";
        getID();
        path = formatFileName(_path);
        updateID();

        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.WriteLine(";Susceptible;Infetcted;Recovered;Dead;Deaths/day;Carrier density;Reproduction number;Stochastic rate");
            sw.Close();
        }
    }

    /// <summary>
    /// updates the id and writes to file to store
    /// </summary>
    public void updateID()
    {
        id++;
        using (StreamWriter sw = new StreamWriter(id_Path))
        {
            sw.WriteLine(id);
            sw.Close();
        }
    }

    /// <summary>
    /// file name formating to add 0s etc for convennience when sorting files
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string formatFileName(string path)
    {
        path += "raw_data/data_";
        if (id < 10)
            path += "00";
        else if (id < 100)
            path += "0";
        path += id.ToString() + ".csv";
        return path;
    }

    /// <summary>
    /// Gets ID from file. If no such file excists a file is created.
    /// </summary>
    private void getID()
    {
        try
        {
            using (StreamReader sr = new StreamReader(id_Path))
            {
                string? s = sr.ReadLine();
                if (s != null)
                    id = int.Parse(s);
                else
                {
                    id = 0;
                    using (StreamWriter sw = new StreamWriter(id_Path))
                        sw.WriteLine(id);
                }
                sr.Close();
            }
        }
        catch (Exception e)
        {
            if (e is FileNotFoundException)
            {
                id = 0;
                using (StreamWriter sw = File.CreateText(id_Path))
                {
                    sw.WriteLine(id);
                    sw.Close();
                }
            }
            else
            {
                Console.WriteLine(e);
            }
        }
        finally
        {
            Console.WriteLine("id stored" + path);
        }
    }


    public void saveData(decimal[] stats, int n)
    {
        string line = n.ToString();
        for (int i = 0; i < stats.Length; i++)
        {
            line += ";" + (stats[i].ToString()).Replace('.', ',') ;
        }
        try
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            using (StreamWriter sw = File.AppendText(path))
            {
                //Write a line of text
                sw.WriteLine(line);
                //Close the file
                sw.Close();
            }
        }
        catch (Exception e)
        {
            if (e is FileNotFoundException)
            {
                using (StreamWriter sw = File.CreateText(id_Path))
                {
                    sw.WriteLine("\" \",\"R\",\"S\",\"I\",\"D\"\n" + line);
                    sw.Close();
                }
            }
            else
                Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
    }
}