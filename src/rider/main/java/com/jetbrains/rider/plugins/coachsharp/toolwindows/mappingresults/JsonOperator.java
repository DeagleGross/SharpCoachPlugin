package com.jetbrains.rider.plugins.coachsharp.toolwindows.mappingresults;

import java.io.*;

class JsonOperator
{
    public static Object Deserialize(String filename)
    {
        try 
        {
            FileInputStream file = new FileInputStream(filename);
            ObjectInputStream in = new ObjectInputStream(file);
            
            var deserializedObject = in.readObject();
            
            in.close();
            file.close();
            
            return deserializedObject;
        }
        catch (IOException ex) {
            return null;
        }
  
        catch (ClassNotFoundException ex) {
            return null;
        }
    }  
}