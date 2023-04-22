package com.example.lab6.repo;

import com.example.lab6.domain.Friendship;
import com.example.lab6.domain.Message;
import com.example.lab6.domain.Pair;
import com.example.lab6.domain.User;

import java.sql.*;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public class MessageRepositoryDatabase implements Repository<Integer, Message>{
    final private String url;
    final private String username;
    final private String password;
    private List<Message> lista = new ArrayList<>();

    public MessageRepositoryDatabase(String url, String username, String password) {
        this.url = url;
        this.username = username;
        this.password = password;
        loadData();
    }
    @Override
    public void save(Message adding)
    {
        String comanda="INSERT INTO public.\"Message\" (\"Sender\",\"Receiver\",\"MessageText\",\"Data trimiterii\",\"Timpul trimiterii\") VALUES (?,?,?,?,?);";
        try (Connection connection = DriverManager.getConnection(url, username, password); PreparedStatement ps = connection.prepareStatement(comanda))
        {
            ps.setInt(1, adding.getIdSender());
            ps.setInt(2, adding.getIdReceiver());
            String[] list = adding.getDataTextului().toString().split("T");
            ps.setDate(4, Date.valueOf(list[0]));
            String[] data=list[1].split(":");
            String curr=data[0]+":"+data[1]+":00";
            ps.setTime(5, Time.valueOf(curr));
            ps.setString(3,adding.getText());
            lista.add(adding);
            ps.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
    @Override
    public void delete(Message user1)
    {
        try (Connection connection = DriverManager.getConnection(url, username, password);Statement ps = connection.createStatement())
        {
            String comanda1="DELETE FROM public.\"Message\" WHERE \"Sender\"="+user1.getIdSender()+" AND \"Receiver\"="+user1.getIdReceiver()+";";
            lista.remove(user1);
            ps.executeUpdate(comanda1);
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
    @Override
    public Message find(Integer userToBeFound) {
        for(Message u:lista)
        {
            if(u.getId().equals(userToBeFound))
            {
                return u;
            }
        }
        return null;
    }
    @Override
    public void update(Message entity, Integer id) {
    }

    @Override
    public List<Message> getList() {
        return lista;
    }
    private void loadData()
    {
        try (Connection connection = DriverManager.getConnection(url, username, password); Statement statement = connection.createStatement(ResultSet.TYPE_SCROLL_SENSITIVE,ResultSet.CONCUR_READ_ONLY);ResultSet resultSet = statement.executeQuery("SELECT * FROM public.\"Message\""))
        {
            while (resultSet.next())
            {
                int id1 = resultSet.getInt("Sender");
                int id2 = resultSet.getInt("Receiver");
                String text = resultSet.getString("MessageText");
                String data_trimiterii = resultSet.getString("Data trimiterii");
                String timpul_trimiterii = resultSet.getString("Timpul trimiterii");
                Message mesaj = new Message(id1,id2,text,LocalDateTime.parse(data_trimiterii+"T"+timpul_trimiterii));
                lista.add(mesaj);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
