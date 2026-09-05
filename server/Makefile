
CC = g++

CFLAGS =  -g -Isrc -fsanitize=address

TARGET = main

SRCS = $(wildcard src/*.cpp)
OBJS = $(SRCS:src/%.cpp=build/%.o)

LIBS = -fsanitize=address



all: $(TARGET)

$(TARGET): $(OBJS)
	$(CC) -o $(TARGET) $(OBJS) $(LIBS)

build/%.o: src/%.cpp | build
	$(CC) $(CFLAGS) -c $< -o $@

build:
	mkdir build

clean:
	rm -rf build/
	rm -f $(TARGET)
