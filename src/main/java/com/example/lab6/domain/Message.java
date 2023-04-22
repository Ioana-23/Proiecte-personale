package com.example.lab6.domain;

import java.time.DateTimeException;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public class Message extends Entity<Integer>{
    private int idSender;
    private int idReceiver;
    private String text;
    private LocalDateTime dataTextului;

    public Message(int idSender, int idReceiver, String text, LocalDateTime dateleTextelor) {
        this.idSender = idSender;
        this.idReceiver = idReceiver;
        this.text = text;
        this.dataTextului = dateleTextelor;
    }

    public int getIdSender() {
        return idSender;
    }

    public int getIdReceiver() {
        return idReceiver;
    }

    public String getText() {
        return text;
    }

    public LocalDateTime getDataTextului() {
        return dataTextului;
    }
}
