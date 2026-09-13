//
// Created by cdlemmi on 05.09.26.
//

#ifndef SEALSOUNDMAKER_CONNECTION_MANAGER_H
#define SEALSOUNDMAKER_CONNECTION_MANAGER_H
#include "http.h"


HTTPRequest readRequest(int clientHandle);


class ConnectionHandler {
private:
    int serverHandle;

    void handleClient(int clientHandle);
public:
    ConnectionHandler();
    virtual ~ConnectionHandler();

    void start(int port);



};



#endif //SEALSOUNDMAKER_CONNECTION_MANAGER_H
