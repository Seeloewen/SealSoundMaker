//
// Created by cdlemmi on 12.09.26.
//

#include <utility>
#include <sys/socket.h>
#include <gtest/gtest.h>
#include <sys/socket.h>

#include "connection_handler.h"
#include "http.h"


std::pair<int, int> createSocketPair() {
    int fds[2];
    int result = socketpair(AF_UNIX, SOCK_STREAM, 0, fds);
    if (result != 0) {
        throw std::runtime_error("socketpair failed");
    }
    return std::make_pair(fds[0], fds[1]);
}



TEST(HTTP_Parsing, Simple1) {
    const std::string http_request =
        "GET /index.html HTTP/1.1\r\n"
        "Host: www.waybersite.de\r\n"
        "User-Agent: Mozilla/5.0\r\n"
        "Accept: text/html\r\n" // , additional maybe
        "Accept-Language: en-US,en;q=0.9\r\n"
        "Connection: keep-alive\r\n"
        "\r\n";

    auto [w, r] = createSocketPair();

    ASSERT_EQ(http_request.size(), write(w, http_request.c_str(), http_request.size()));

    HTTPRequest request = readRequest(r);

    ASSERT_EQ(request.method, GET);
    ASSERT_EQ(request.uri, "/index.html");

    close(w);
    close(r);
}


TEST(HTTP_Parsing, Cookie1) {
    const std::string http_request =
        "GET /index.html HTTP/1.1\r\n"
        "Host: www.waybersite.de\r\n"
        "User-Agent: Mozilla/5.0\r\n"
        "Accept: text/html\r\n" // , additional maybe
        "Accept-Language: en-US,en;q=0.9\r\n"
        "Cookie: session=ABCDEF50\r\n"
        "Connection: keep-alive\r\n"
        "\r\n";

    auto [w, r] = createSocketPair();

    ASSERT_EQ(http_request.size(), write(w, http_request.c_str(), http_request.size()));

    HTTPRequest request = readRequest(r);

    ASSERT_EQ(request.method, GET);
    ASSERT_EQ(request.uri, "/index.html");
    std::string c1 = request.cookies["session"];
    ASSERT_EQ(c1, "ABCDEF50");

    close(w);
    close(r);
}

TEST(HTTP_Parsing, Cookie2) {
    const std::string http_request =
        "GET /index.html HTTP/1.1\r\n"
        "Host: www.waybersite.de\r\n"
        "User-Agent: Mozilla/5.0\r\n"
        "Accept: text/html\r\n" // , additional maybe
        "Accept-Language: en-US,en;q=0.9\r\n"
        "Cookie: hello=world session=ABCDEF50 c=++\r\n"
        "Connection: keep-alive\r\n"
        "\r\n";

    auto [w, r] = createSocketPair();

    ASSERT_EQ(http_request.size(), write(w, http_request.c_str(), http_request.size()));

    HTTPRequest request = readRequest(r);

    ASSERT_EQ(request.method, GET);
    ASSERT_EQ(request.uri, "/index.html");
    std::string c1 = request.cookies["session"];
    ASSERT_EQ(c1, "ABCDEF50");

    close(w);
    close(r);
}

