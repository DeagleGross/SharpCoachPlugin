package com.example.app;

import java.io.Serializable;

public class MappingResult implements Serializable {
    public String inputClassName;
    public String outputClassName;
    public String operationDate;

    public MappingResult(String inputClassName, String outputClassName, String operationDate){
        this.inputClassName = inputClassName;
        this.outputClassName = outputClassName;
        this.operationDate = operationDate;
    }
}
