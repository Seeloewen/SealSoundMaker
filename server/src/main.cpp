
#include <connection_handler.h>

#include <iostream>


int main() {

    std::cout << "SealSoundMaker Server started!" << std::endl;


    ConnectionHandler connectionManager = ConnectionHandler();
    connectionManager.start(8080);



    std::cout << "program finished!" << std::endl;
    return EXIT_SUCCESS;

}

