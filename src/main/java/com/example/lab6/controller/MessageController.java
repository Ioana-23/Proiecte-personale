package com.example.lab6.controller;

import com.example.lab6.domain.Message;
import com.example.lab6.domain.User;
import com.example.lab6.events.UserChangeEvent;
import com.example.lab6.observer.Observer;
import com.example.lab6.service.Service;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.Border;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.scene.shape.Box;
import javafx.scene.text.Font;
import javafx.stage.Stage;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

public class MessageController implements Observer<UserChangeEvent>
{
    @FXML
    private TextField typeField;
    @FXML
    private Label labelTime;
    @FXML
    private ListView labelMessUserTalkingTo;

    @FXML
    private SplitPane PanelMessagesUserCurent;

    @FXML
    private ListView labelMessUserCurent = new ListView<>();
    private User userCurent;

    private User userTalkingTo;

    private User getUserCurent() {
        return userCurent;
    }
    @FXML
    private Button btnSend;
    private Service serv;

    public void setUserCurent(User userCurent) {
        this.userCurent = userCurent;
    }

    public void setUserTalkingTo(User userTalkingTo)
    {
        this.userTalkingTo = userTalkingTo;
    }

    public void setUserServ(Service serv)
    {
        this.serv = serv;
        serv.addObserver(this);
        initModel();
    }

    public void initialize()
    {
//        tableColumnPassword.setCellValueFactory(new PropertyValueFactory<User, String>("Parola"));
//        tableColumnName.setCellValueFactory(new PropertyValueFactory<User, String>("Nume"));
//        tableColumnFriendsName.setCellValueFactory(new PropertyValueFactory<User,String>("Nume"));
//
//        tableFriendView.setItems(modelFriends);
//        tableUserView.setItems(modelUser);
    }
    public void initModel()
    {
        labelTime.setFont(new Font("Arial",11));
        List<Message> listaMesaje = serv.getMessageList().stream().filter(x->{return (x.getIdSender() == userCurent.getId() && x.getIdReceiver() == userTalkingTo.getId()) ||
                (x.getIdSender() == userTalkingTo.getId() && x.getIdReceiver()==userCurent.getId());}).toList();
        List<Message> listaMesajeOrdonate = new ArrayList<>();
        listaMesajeOrdonate.addAll(listaMesaje);
        listaMesajeOrdonate.sort(Comparator.comparing(Message::getDataTextului));
        int i=0;
        for (Message mesajCurent : listaMesajeOrdonate)
        {
            Label mesaj1 = new Label();
            mesaj1.setText(mesajCurent.getText());
            mesaj1.setFont(new Font("Arial",11));
            mesaj1.setOnMouseEntered(new EventHandler<MouseEvent>()
            {
                @Override
                public void handle(MouseEvent mouseEvent)
                {
                    labelTime.setText(mesajCurent.getDataTextului().toString().split("T")[1]);
                    labelTime.setVisible(true);
                }
            });
            mesaj1.setOnMouseExited(new EventHandler<MouseEvent>() {
                @Override
                public void handle(MouseEvent mouseEvent) {
                    labelTime.setVisible(false);
                }
            });
            Label aux = new Label();
            aux.setVisible(false);
            if(mesajCurent.getIdSender() == userCurent.getId())
            {
                labelMessUserCurent.getItems().add(mesaj1);
                labelMessUserTalkingTo.getItems().add(aux);
            }
            else
            {
                labelMessUserTalkingTo.getItems().add(mesaj1);
                labelMessUserCurent.getItems().add(aux);
            }
        }
        typeField.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent mouseEvent) {
                typeField.setText("");
            }
        });
        btnSend.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent actionEvent) {
                serv.addMessageService(userCurent.getId(),userTalkingTo.getId(),typeField.getText(),LocalDateTime.now());
                Label mesaj1 = new Label();
                mesaj1.setText(typeField.getText());
                typeField.setText("");
                labelMessUserCurent.getItems().add(mesaj1);
            }
        });
    }

    @Override
    public void update(UserChangeEvent userChangeEvent) {

    }
}
