package com.example.lab6.domain;

import java.time.LocalDateTime;

public class Request {
    private User user;
    private LocalDateTime data;

    public Request(User user, LocalDateTime data) {
        this.user = user;
        this.data = data;
    }

    public void setUser(User user) {
        this.user = user;
    }

    public void setData(LocalDateTime data) {
        this.data = data;
    }

    public User getUser() {
        return user;
    }

    public LocalDateTime getData() {
        return data;
    }
    public String getParola()
    {
        return this.user.getParola();
    }
    public String getNume()
    {
        return this.user.getNume();
    }
}
