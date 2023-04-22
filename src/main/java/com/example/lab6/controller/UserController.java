package com.example.lab6.controller;

import com.example.lab6.HelloApplication;
import com.example.lab6.domain.Friendship;
import com.example.lab6.domain.Pair;
import com.example.lab6.domain.User;
import com.example.lab6.events.UserChangeEvent;
import com.example.lab6.observer.Observer;
import com.example.lab6.service.Service;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.effect.DropShadow;
import javafx.scene.image.Image;
import javafx.scene.input.KeyEvent;
import javafx.scene.input.MouseEvent;
import javafx.scene.text.TextAlignment;
import javafx.stage.Stage;
import org.kordamp.bootstrapfx.scene.layout.Panel;

import java.io.IOException;
import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;

public class UserController implements Observer<UserChangeEvent> {
    @FXML
    private Button btnMessage;
    @FXML
    private Button hover3;
    @FXML
    private Button hover2;
    @FXML
    private Button hover1;
    @FXML
    private Button btnFriendReq;
    @FXML
    private Button btnDel;
    @FXML
    private Button btnAdd;
    @FXML
    private TableColumn<User,String> tableColumnFriendsName;
    @FXML
    private TableView<User> tableUserView;
    private Service serv;
    private ObservableList<User> modelFriends = FXCollections.observableArrayList();
    private ObservableList<User> modelUser = FXCollections.observableArrayList();
    @FXML
    private TableView<User> tableFriendView;
    @FXML
    private TextField searchField;
    private User userCurent;
    @FXML
    private Label anunt;
    @FXML
    private TableColumn<User,String> tableColumnName;
    @FXML
    private TableColumn<User,String> tableColumnPassword;

    @FXML
    private Label contCurent;
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
        tableColumnPassword.setCellValueFactory(new PropertyValueFactory<User, String>("Parola"));
        tableColumnName.setCellValueFactory(new PropertyValueFactory<User, String>("Nume"));
        tableColumnFriendsName.setCellValueFactory(new PropertyValueFactory<User,String>("Nume"));

        tableFriendView.setItems(modelFriends);
        tableUserView.setItems(modelUser);
    }
    @Override
    public void update(UserChangeEvent userChangeEvent)
    {
        initModel();
        initialize();
    }
    public void initModel()
    {
        btnFriendReq.setDisable(true);
        anunt.setVisible(false);
        contCurent.setText(userCurent.getNume());
        modelFriends.setAll();
        modelUser.setAll();
        List<User> lista = serv.getUserFriends(userCurent);
        hover1.setOpacity(0);
        hover2.setOpacity(0);
        hover3.setOpacity(0);
        contCurent.setEffect(new DropShadow());
        anunt.setLayoutX(btnFriendReq.getLayoutX());
        anunt.setLayoutY(btnFriendReq.getLayoutY());
        btnMessage.setDisable(true);
        int id=0;
        for(Friendship f: serv.getFriendsList().stream().filter(x -> x.isAcceptat()).toList())
        {
            if(!f.getU2().equals(f.getU1()))
            {
                if(f.getU1().equals(userCurent))
                {
                    modelFriends.add(f.getU2());
                }
                if(f.getU2().equals(userCurent))
                {
                    modelFriends.add(f.getU1());
                }
            }
        }
        for(User u:serv.getUserList())
        {
            Friendship fr = new Friendship(userCurent,u,LocalDateTime.now());
            fr.setId(new Pair<>(userCurent.getId(),u.getId()));
            Friendship fr2 = new Friendship(u,userCurent,LocalDateTime.now());
            fr2.setId(new Pair<>(u.getId(),userCurent.getId()));
            List<Friendship> frL =serv.getFriendsList().stream().filter(x->x.equals(fr)).filter(x->!x.isAcceptat()).toList();
            List<Friendship> frAux=serv.getFriendsList().stream().filter(x -> x.equals(fr2)).toList();
            List<Friendship> frAux2 = serv.getFriendsList().stream().filter(x -> x.equals(fr)).toList();
            List<Friendship> frReq = frAux.stream().filter(x -> !x.isAcceptat()).toList();
            List<Friendship> frReq2 = frAux2.stream().filter(x->!x.isAcceptat()).toList();
            if(!serv.getFriendsList().contains(fr) && !serv.getFriendsList().contains(fr2) && !userCurent.equals(u))
            {
                modelUser.add(u);
            }
            if(serv.getFriendsList().contains(fr) && frL.size()==1 && !userCurent.equals(u))
            {
                modelUser.add(u);
            }
            if(frReq.size()==1 || frReq2.size()==1)
            {
                btnFriendReq.setDisable(false);
            }
        }
    }
    public void handleReqHoverButton1(MouseEvent actionEvent)
    {
        if(btnFriendReq.isDisable())
        {
            anunt.setText("Nu exista cereri de prietenie");
            anunt.setVisible(true);
        }
    }
    public void handleReqHoverButton2(MouseEvent actionEvent)
    {
        if(btnAdd.isDisable())
        {
            anunt.setText("Selectati un utlizatorul!");
            anunt.setVisible(true);
        }
    }
    public void handleReqHoverButton3(MouseEvent actionEvent)
    {
        if(btnDel.isDisable())
        {
            anunt.setText("Selectati un prieten!");
            anunt.setVisible(true);
        }
    }
    public void handleAddFriend(ActionEvent actionEvent)
    {
        User user = tableUserView.getSelectionModel().getSelectedItem();
        //modelFriends.setAll(serv.getUserFriends(userCurent));
        Friendship fr = new Friendship(user,userCurent,LocalDateTime.now());
        fr.setId(new Pair<>(user.getId(),userCurent.getId()));
        if(serv.getFriendsList().stream().filter(x->x.getId().equals(fr.getId())).toList().size()!=0)
        {
            MessageAlert.showErrorMessage(null,"Deja exista cerere de prietenie!");
            return;
        }
        serv.addFriendService(userCurent.getId(),user.getId(), LocalDateTime.now());
        MessageAlert.showMessage(null,"Cererea de prietnie s-a trimis cu succes!");
        update(null);
    }
    public void handleActivateButtonAdd(MouseEvent actionEvent)
    {
        btnDel.setDisable(true);
        btnAdd.setDisable(false);
        btnMessage.setDisable(false);
        tableFriendView.getSelectionModel().clearSelection();

    }
    public void handleActivateButtonDelete(MouseEvent actionEvent)
    {
        tableUserView.getSelectionModel().clearSelection();
        btnAdd.setDisable(true);
        btnMessage.setDisable(false);
        btnDel.setDisable(false);
    }
    public void messageUser(ActionEvent actionEvent) throws IOException
    {
        User user = new User();
        if(!btnAdd.isDisable())
        {
            user = tableUserView.getSelectionModel().getSelectedItem();
        }
        else
        {
            user = tableFriendView.getSelectionModel().getSelectedItem();
        }
        FXMLLoader fxmlLoader = new FXMLLoader(HelloApplication.class.getResource("views/MessageView.fxml"));
        Scene scene = new Scene(fxmlLoader.load(), 700, 450);
        Stage stage = new Stage();
        stage.getIcons().add(new Image("com/example/lab6/images/icon.jpg"));
        stage.setTitle("Message:"+user.getNume());
        stage.setScene(scene);
        stage.show();
        MessageController messageController = fxmlLoader.getController();
        messageController.setUserCurent(userCurent);
        messageController.setUserTalkingTo(user);
        messageController.setUserServ(this.serv);
    }
    public void handleDeleteUser(ActionEvent actionEvent)
    {
        User user = tableFriendView.getSelectionModel().getSelectedItem();
        serv.removeFriendService(userCurent.getId(),user.getId(),LocalDateTime.now());
        update(null);
    }
    public void handleSearchUser(KeyEvent actionEvent)
    {
        update(null);
        tableUserView.setItems(modelUser);
        String valoareNume = String.valueOf(searchField.getCharacters());
        if(!valoareNume.equals(""))
        {
            List<User> fr = modelUser;
            List<User> prieteni = fr.stream().filter(x -> x.getNume().contains(valoareNume)).toList();
            modelUser.setAll(prieteni);
            tableUserView.setItems(modelUser);
        }
        btnAdd.setDisable(true);
    }
    public void handleLogOut(ActionEvent actionEvent) throws IOException {
        FXMLLoader fxmlLoader = new FXMLLoader(HelloApplication.class.getResource("views/LoginView.fxml"));
        Scene scene = new Scene(fxmlLoader.load(), 300, 300);
        Stage stage = new Stage();
        stage.setTitle("Login Window");
        stage.getIcons().add(new Image("com/example/lab6/images/icon.jpg"));
        stage.setScene(scene);
        stage.show();
        LoginController loginController = fxmlLoader.getController();
        loginController.setUserServ(this.serv);
        ((Node)(actionEvent.getSource())).getScene().getWindow().hide();
    }
    public void handleClickSearch(MouseEvent actionEvent)
    {
        searchField.setText("");
    }
    public void handleShowFriendRequest(ActionEvent actionEvent) throws IOException {
        FXMLLoader fxmlLoader = new FXMLLoader(HelloApplication.class.getResource("views/FriendReqView.fxml"));
        Scene scene = new Scene(fxmlLoader.load(), 650, 450);
        Stage stage = new Stage();
        stage.getIcons().add(new Image("com/example/lab6/images/icon.jpg"));
        stage.setTitle("Friend Requests");
        stage.setScene(scene);
        stage.show();
        FriendRequestController friendRequestController = fxmlLoader.getController();
        friendRequestController.setController(this);
        friendRequestController.setUserCurent(userCurent);
        friendRequestController.setUserServ(this.serv);
    }
    public void handleRezCancelHover(MouseEvent mouseEvent) {
        anunt.setVisible(false);
    }
}
