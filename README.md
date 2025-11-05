# 🤖 AI Commit Generator (.NET Global Tool)

### `aicommit`

This is a powerful command-line utility built with **.NET 8** and the **Google Gemini API**. It instantly generates concise, high-quality, and conventionally-formatted Git commit messages based on your staged code changes (`git diff --staged`).

It transforms a manual and often time-consuming step into a single, automated command.

---

## ✨ Features

* **AI-Powered:** Uses the `gemini-2.5-flash` model for intelligent analysis of code diffs.
* **Conventional Commits:** Generates messages strictly following the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) specification (e.g., `feat: added user profile endpoint`).
* **Global Tool:** Once installed, it can be executed from **any directory** on your system using the simple command `aicommit`.
* **Secure:** Uses .NET User Secrets to securely handle your Gemini API key during development.

---

## 🚀 Installation and Setup

### Prerequisites

1.  **A Gemini API Key:** Get one from Google AI Studio.
2.  **Git:** Installed and available in your terminal.
3.  **.NET 8 SDK:** (or newer) installed on your system.

### Step 1: Securely Store Your API Key

The tool requires your Gemini API key to be available as an environment variable or stored in .NET User Secrets. For local use, User Secrets is highly recommended.

1.  Navigate to the `GitCommitGen` project folder in your terminal:
    ```bash
    cd C:\GitCommitGen
    ```
2.  Initialize User Secrets (if you haven't already):
    ```bash
    dotnet user-secrets init
    ```
3.  Set your API key:
    ```bash
    dotnet user-secrets set "GEMINI_API_KEY" "YOUR_API_KEY_HERE"
    ```

### Step 2: Build and Install the Global Tool

Run these two commands from the **`GitCommitGen`** directory to package the application and install it globally:

1.  **Pack the Project (creates the installable package):**
    ```bash
    dotnet pack -c Release
    ```
2.  **Install the Global Tool (named `aicommit`):**
    ```bash
    dotnet tool install --global --add-source ./nupkg GitCommitGen
    ```
    *(If you get an error that the tool is already installed, use `dotnet tool update` instead of `dotnet tool install`.)*

You should see a confirmation that the tool was successfully installed and can be invoked using `aicommit`.

---

## 💡 Usage

Once installed, use the command **`aicommit`** by piping (`|`) the output of `git diff --staged` into it. This command is run from **inside your target Git repository**.

### 1. Stage Your Changes

In your project folder, make and stage your file changes:

```bash
git add .
or
git add -A
```

### 2. Generate and Copy the Commit Message

Pipe the staged diff into the tool:

```bash
git diff --staged | aicommit
```
The tool will print the suggested commit message, for example:
```
🤖 Generating commit message based on your diff...

Suggested Commit Message:
feat: implement secure api key loading via user secrets
```

### 3. Commit

Copy the suggested message and use it directly:

```bash
git commit -m "feat: implement secure api key loading via user secrets"
```

---

## 🧑‍💻 Contributing and Development

If you wish to work on the source code, you must manually update the global tool whenever you make changes.

1.  **Make changes to the code.**
    
2.  **Repack the project:**
    ```bash
    dotnet pack -c Release
    ```
3.  **Update the global tool:**
    ```bash
    dotnet tool update --global --add-source ./nupkg GitCommitGen
    ```
