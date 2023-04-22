package com.example.lab6.controller;

import com.example.lab6.HelloApplication;
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
import javafx.scene.input.KeyEvent;
import javafx.stage.Stage;
import org.w3c.dom.Text;

import java.io.IOException;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

public class LoginController implements Observer<UserChangeEvent>
{
    @FXML
    private Label incorect;
    Service serv;
    List<User> users;
    @FXML
    private TextField usernameField;
    @FXML
    private PasswordField passwordField;
    private User userCurent;
    public void setUserServ(Service serv)
    {
        this.serv = serv;
        serv.addObserver(this);
    }
    @FXML
    public void initialize()
    {

    }

    public void handleLoginButton(ActionEvent actionEvent) throws IOException {
        users = serv.getUserList();
        boolean exista = false;
        String valoareNume = String.valueOf(usernameField.getCharacters());
        String valoareParola = String.valueOf(passwordField.getCharacters());
        User utAux = new User(valoareNume,valoareParola);
        if(incorect.getText().equals("Utilizatorul exista deja!"))
        {
            incorect.setVisible(false);
            return;
        }
        for(User ut:users)
        {
            if(ut.equals(utAux))
            {
                exista=true;
                userCurent=ut;
                FXMLLoader fxmlLoader = new FXMLLoader(HelloApplication.class.getResource("views/UserView.fxml"));
                Scene scene = new Scene(fxmlLoader.load(), 800, 600);
                Stage stage = new Stage();
                stage.setTitle("User Window");
                stage.setScene(scene);
                stage.show();
                UserController userController = fxmlLoader.getController();
                userController.setUserCurent(userCurent);
                userController.setUserServ(this.serv);
                ((Node)(actionEvent.getSource())).getScene().getWindow().hide();
            }
            if(ut.getNume().equals(utAux.getNume()) && !ut.getParola().equals(utAux.getParola()))
            {
                exista=true;
                incorect.setText("Parola gresita!");
                incorect.setVisible(true);
            }
        }
        if(!exista)
        {
            incorect.setText("Nu exista utilizatorul!");
            incorect.setVisible(true);
        }
    }
    public void handleSignUpButton(ActionEvent actionEvent) throws IOException {
        String valoareNume = usernameField.getText();
        String valoareParola = passwordField.getText();
        if(incorect.getText().equals("Nu exista utilizatorul!"))
        {
            incorect.setVisible(false);
        }
        if(serv.getUserList().contains(new User(valoareNume,valoareParola)))
        {
            incorect.setText("Utilizatorul exista deja!");
            incorect.setVisible(true);
            return;
        }
        serv.addUserService(valoareNume,valoareParola);
        users = serv.getUserList();
        List<User> lista = serv.getUserList();
        User utAux = new User(valoareNume,valoareParola);
        for(User ut:users)
        {
            if(ut.equals(utAux))
            {
                userCurent=ut;
                FXMLLoader fxmlLoader = new FXMLLoader(HelloApplication.class.getResource("views/UserView.fxml"));
                Scene scene = new Scene(fxmlLoader.load(), 800, 600);
                Stage stage = new Stage();
                stage.setTitle("New Window");
                stage.setScene(scene);
                stage.show();
                UserController userController = fxmlLoader.getController();
                userController.setUserCurent(userCurent);
                userController.setUserServ(this.serv);
                ((Node)(actionEvent.getSource())).getScene().getWindow().hide();
            }
            }

    }
    @Override
    public void update(UserChangeEvent userChangeEvent)
    {
        initialize();
    }
    public void handleVisibleIncorrect(KeyEvent keyEvent)
    {
        if(passwordField.getText().length()==0)
        {
            incorect.setVisible(false);
        }
    }
}
