package com.example.app;

import com.google.gson.Gson;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

public class JsonOperator {
    public static MappingResult[] DeserializeToMappingResults(String filename){
        try
        {
            Gson g = new Gson();
            var serialized = Files.readString(Path.of(filename));
            return g.fromJson(serialized, MappingResult[].class);
        }
        catch (IOException ex) {
            return null;
        }
    }
}