package com.example.app;

import java.io.Serializable;

public class MappingResult implements Serializable {
    public String inputClassName;
    public String outputClassName;
    public String operationDate;

    public String[] failedInputProperties;
    public String[] failedOutputProperties;

    public MappingResult(String inputClassName, String outputClassName, String operationDate, String[] failedInputProperties, String[] failedOutputProperties){
        this.inputClassName = inputClassName;
        this.outputClassName = outputClassName;
        this.operationDate = operationDate;

        this.failedInputProperties = failedInputProperties;
        this.failedOutputProperties = failedOutputProperties;
    }
}
