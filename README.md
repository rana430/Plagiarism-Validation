# Plagiarism Validation Project

## Overview

The Plagiarism Validation Project is designed to analyze and compare pairs of files to detect similarities. The project reads Excel files from a specified folder, processes them to build graphs, constructs Maximum Spanning Trees (MST) from these graphs, and outputs the results to new Excel files. This tool is useful for detecting and analyzing potential plagiarism by comparing file contents.

## Features

- Reads multiple Excel files from specified folders.
- Analyzes and constructs graphs from file pairs.
- Builds Minimum Spanning Trees (MST) from the constructed graphs.
- Outputs analysis results to new Excel files.
- Provides performance timing for each stage of processing.

## Prerequisites

- .NET Framework
- EPPlus library for Excel file manipulation

## Setup

1. **Clone the repository:**

    ```bash
    git clone https://github.com/your-username/plagiarism-validation.git
    cd plagiarism-validation
    ```

2. **Install dependencies:**

    Make sure you have the EPPlus library installed. If not, you can install it via NuGet Package Manager:

    ```bash
    Install-Package EPPlus
    ```

3. **Update the file paths:**

    Modify the `testTypes` dictionary in `Program.cs` to reflect the correct paths on your machine where the test cases are located.

## Usage

1. **Compile the project:**

    Open the project in your preferred .NET IDE (such as Visual Studio) and compile it.

2. **Run the project:**

    Execute the compiled program. You will be prompted to select a test case type and, if applicable, a test level.

3. **Select options:**

    - Choose the test case type by entering `1` for Sample or `2` for Complete.
    - If you selected Complete, choose the test level by entering `1` for Easy, `2` for Medium, or `3` for Hard.

4. **View results:**

    The program will process the files in the specified folder, generate MST and statistics Excel files, and display the processing times for each stage.

5. **Repeat or exit:**

    After processing the files, you can choose to process another folder by entering `y` or exit the program by entering `n`.

## File Structure

- **Program.cs:** Main entry point of the application. Handles user input and orchestrates file processing.
- **TestFile.cs:** Defines the `TestFile` class, which holds metadata and timing information for each test file.
- **Node.cs:** Defines the `Node` class, which represents a node in the graph.
- **Edge.cs:** Defines the `Edge` class, which represents an edge between two nodes.
- **Excel.cs:** Contains methods to read pairs from Excel files.
- **GroupStat.cs:** Contains methods to build graphs and construct components from file pairs.
- **Component.cs:** Defines the `Component` class, which represents a connected component in the graph.
- **MST.cs:** Contains methods to construct MSTs from graph components.
- **MSTExcelWriter.cs:** Contains methods to write MST results to an Excel file.
- **StatExcelWriter.cs:** Contains methods to write statistical analysis results to an Excel file.
