cmake_minimum_required(VERSION 3.13)
project(YogaNative CXX)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_POSITION_INDEPENDENT_CODE ON)

# Source files for Yoga
file(GLOB YOGA_SOURCES 
    "${CMAKE_CURRENT_SOURCE_DIR}/src/yoga/*.cpp"
    "${CMAKE_CURRENT_SOURCE_DIR}/src/yoga/**/*.cpp"
)

# Create a shared library
add_library(yoga SHARED ${YOGA_SOURCES})

# Include directories
target_include_directories(yoga PUBLIC "${CMAKE_CURRENT_SOURCE_DIR}/src")

# Definitions
if(WIN32)
    target_compile_definitions(yoga PRIVATE _WINDLL)
endif()
