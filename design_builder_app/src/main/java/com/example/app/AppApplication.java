package com.example.app;

import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.builder.SpringApplicationBuilder;

import javax.swing.*;
import java.awt.*;

@SpringBootApplication
public class AppApplication implements CommandLineRunner {

    public static void main(String[] args) {
        new SpringApplicationBuilder(AppApplication.class).headless(false).run(args);
    }

    @Override
    public void run(String... args) {
        JFrame frame = new JFrame("Spring Boot Swing App");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(500,700);
        var panel = getContent();
        frame.setContentPane(panel);
        frame.setVisible(true);
    }

    private JPanel getContent(){
        var contentManager = new ContentManager();
        return contentManager.getContent();
    }
}
