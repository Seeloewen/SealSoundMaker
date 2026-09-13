
CC = g++

CFLAGS =  -g -Isrc -fsanitize=address

TARGET = main

SRCS = $(wildcard src/*.cpp)
OBJS = $(SRCS:src/%.cpp=build/%.o)

LIBS = -fsanitize=address

TEST_TARGET = run_tests
TEST_SRC = test/test.cpp
GTEST_LIBS := -lgtest -lgtest_main -pthread
LIB_OBJS = $(filter-out build/main.o,$(OBJS))

all: $(TARGET)

$(TARGET): $(OBJS)
	$(CC) -o $(TARGET) $(OBJS) $(LIBS)

build/%.o: src/%.cpp | build
	# $< = first prerequisite| $@ target of this rule
	$(CC) $(CFLAGS) -c $< -o $@

build:
	mkdir build


test: $(TEST_TARGET)

$(TEST_TARGET): $(TEST_SRC) $(LIB_OBJS)
	$(CC) $(CFLAGS) $(TEST_SRC) -o $(TEST_TARGET) $(LIB_OBJS) $(GTEST_LIBS)



clean:
	rm -rf build/
	rm -f $(TARGET)
