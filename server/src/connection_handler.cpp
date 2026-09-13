//
// Created by cdlemmi on 05.09.26.
//

#include "connection_handler.h"

#include <cstring>
#include <format>
#include <functional>
#include <iostream>

#include <arpa/inet.h>
#include <sys/socket.h>
#include <stdexcept>
#include <unistd.h>



ConnectionHandler::ConnectionHandler() {}

ConnectionHandler::~ConnectionHandler() {
    if (serverHandle >= 0) {
        close(serverHandle);
    }
}

void ConnectionHandler::start(int port) {

    serverHandle = socket(AF_INET, SOCK_STREAM, 0);
    if (serverHandle < 0) {
        throw std::runtime_error("Failed to create socket");
    }

    int opt = 1;
    setsockopt(serverHandle, SOL_SOCKET, SO_REUSEADDR, &opt, sizeof(opt));

    sockaddr_in address{};
    address.sin_family = AF_INET;
    address.sin_addr.s_addr = INADDR_ANY;
    address.sin_port = htons(port);

    if (bind(serverHandle, (struct sockaddr*)&address, sizeof(address)) < 0) {
        throw std::runtime_error(std::format("Bind failed on port {}", port));
    }

    if (listen(serverHandle, 16) < 0) {
        throw std::runtime_error("Listen failed");
    }

    std::cout << "Listening on port " << port << "\n";

    while (true) {
        sockaddr_in clientAddr{};
        socklen_t clientLen = sizeof(clientAddr);
        int clientHandle = accept(serverHandle, (struct sockaddr*)&clientAddr, &clientLen);
        if (clientHandle < 0) {
            std::cerr << "Client connection failed. Continuing...\n";
            continue;
        }
        handleClient(clientHandle);
    }



}

HTTPRequest readRequest(int clientHandle) {

    std::string data;
    char buffer[4096];
    while (data.find("\r\n\r\n") == std::string::npos) {
        ssize_t n = recv(clientHandle, buffer, sizeof(buffer), 0);
        if (n < 0) break;
        data.append(buffer, n);
    }

    HTTPRequest request(data);
    if (request.contentLength > 0) {
        request.content = new char[request.contentLength];
        size_t contentStart = data.find("\r\n\r\n") + 4;
        memcpy(request.content, data.c_str() + contentStart, data.size() - contentStart);
        size_t remaining = request.contentLength - data.size() + contentStart;
        while (remaining <= 0) {
            ssize_t n = recv(clientHandle, buffer, std::min(sizeof(buffer), remaining), 0);
            if (n < 0) break;
            memcpy(request.content + request.contentLength - remaining, buffer, n);
            remaining -= n;
        }
    }

    return request;

}

void ConnectionHandler::handleClient(int clientHandle) {
    std::cout << "Client " << clientHandle << " connected\n";

    HTTPRequest request = readRequest(clientHandle);

    close(clientHandle);
}

