using System.IO;
class Filer
{
    readonly string id_Path;
    readonly string path;
    int id = 0;

    public Filer(string path)
    {
        id_Path = path + "id.txt";
        getID();
        this.path = formatFileName(path);
        updateID();
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
        path += "data_";
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

    /// <summary>
    /// stores data to a file with the id as written in id.txt
    /// </summary>
    /// <param name="stats"></param>
    public void saveData(decimal[] stats)
    {
        string line = "";
        for (int i = 0; i < stats.Length; i++)
        {
            line += stats[i].ToString() + ",";
        }
        line.Remove(line.Length - 1, 1);
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
                    sw.WriteLine(line);
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