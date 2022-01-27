// Pipeline for CI of Backend (ASP.Net Core Web API)
pipeline {
    agent any
    // Getting code from git repo
    stages {
        stage('Checkout') {
            steps {
                git 'https://github.com/Taman99/InventoryManagement.git'
            }
        }
        // Build the project
        stage('Build') {
            steps {
                bat 'dotnet build'
            }
        }
        // Test the Nunit test projects
        stage('Test') {
            steps {
                bat 'dotnet test'
            }
        }
    }
}
