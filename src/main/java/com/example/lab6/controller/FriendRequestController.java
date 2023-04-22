package com.example.lab6.controller;

import com.example.lab6.domain.Friendship;
import com.example.lab6.domain.Pair;
import com.example.lab6.domain.Request;
import com.example.lab6.domain.User;
import com.example.lab6.events.UserChangeEvent;
import com.example.lab6.observer.Observer;
import com.example.lab6.service.Service;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.Node;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;

import java.time.LocalDateTime;
import java.util.List;

public class FriendRequestController implements Observer<UserChangeEvent> {
    @FXML
    private TableColumn<Request,LocalDateTime> tableColumnFriendsDate;
    @FXML
    private TableColumn<Request,String> tableColumnPasswordReq;
    @FXML
    private TableColumn<Request,String> tableColumnNameReq;
    @FXML
    private TableView<Request> tableFriendReqView;
    Service serv;
    private User userCurent;
    private ObservableList<Request> modelFriendRequest = FXCollections.observableArrayList();

    private UserController controller;

    public void setController(UserController controller) {
        this.controller = controller;
    }

    @FXML
    public void setUserCurent(User userCurent) {
        this.userCurent = userCurent;
    }
    public void setUserServ(Service serv)
    {
        this.serv = serv;
        serv.addObserver(this);
        initModel();
    }
    public void initialize()
    {
        tableColumnPasswordReq.setCellValueFactory(new PropertyValueFactory<Request, String>("Parola"));
        tableColumnNameReq.setCellValueFactory(new PropertyValueFactory<Request, String>("Nume"));
        tableColumnFriendsDate.setCellValueFactory(new PropertyValueFactory<Request,LocalDateTime>("Data"));

        tableFriendReqView.setItems(modelFriendRequest);
    }
    private void initModel()
    {
        modelFriendRequest.setAll();
        List<Friendship> lista = serv.getFriendsList().stream().filter(x -> !x.isAcceptat()).filter(x->x.getU2().equals(userCurent)).toList();
        for(Friendship f:lista)
        {
            if(f.getU1().equals(userCurent))
            {
                Request r = new Request(f.getU2(),f.getData());
                modelFriendRequest.add(r);
            }
            if(f.getU2().equals(userCurent))
            {
                Request r = new Request(f.getU1(),f.getData());
                modelFriendRequest.add(r);
            }
        }
        List<Friendship> lista2 = serv.getFriendsList().stream().filter(x -> !x.isAcceptat()).filter(x->x.getU1().equals(userCurent)).toList();
        for(Friendship f:lista2)
        {
            if(f.getU1().equals(userCurent))
            {
                Request r = new Request(f.getU2(),f.getData());
                modelFriendRequest.add(r);
            }
            if(f.getU2().equals(userCurent))
            {
                Request r = new Request(f.getU1(),f.getData());
                modelFriendRequest.add(r);
            }
        }
    }

    @Override
    public void update(UserChangeEvent userChangeEvent) {
        initModel();initialize();
    }
    public void handleAcceptRequest(ActionEvent actionEvent)
    {
        User user = (User) tableFriendReqView.getSelectionModel().getSelectedItem().getUser();
        user.setId(serv.getUserList().stream().filter(x->x.equals(user)).toList().get(0).getId());
        Friendship friend = new Friendship(user,userCurent, LocalDateTime.now());
        friend.setId(new Pair<>(user.getId(),userCurent.getId()));
        List<Friendship> lista = serv.getFriendsList().stream().filter(x->x.equals(friend)).toList();
        for(Friendship f:serv.getFriendsList())
        {
            if(f.equals(lista.get(0)))
            {
                f.setAcceptat(true);
            }
        }
        serv.setAcceptatFriendService(friend);
        update(null);
        controller.update(null);
        if(modelFriendRequest.size()==0)
        {
            ((Node)(actionEvent.getSource())).getScene().getWindow().hide();

        }
    }
    public void handleDeleteRequest(ActionEvent actionEvent)
    {
        User user = (User) tableFriendReqView.getSelectionModel().getSelectedItem().getUser();
        user.setId(serv.getUserList().stream().filter(x->x.equals(user)).toList().get(0).getId());
        Friendship friend = new Friendship(userCurent,user, LocalDateTime.now());
        friend.setId(new Pair<>(userCurent.getId(),user.getId()));
        List<Friendship> lista = serv.getFriendsList().stream().filter(x->x.equals(friend)).toList();
        serv.removeFriendService(friend.getU1().getId(),friend.getU2().getId(),friend.getData());
        update(null);
        controller.update(null);
        if(modelFriendRequest.size()==0)
        {
            ((Node)(actionEvent.getSource())).getScene().getWindow().hide();
        }
    }
}
