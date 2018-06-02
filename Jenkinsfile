pipeline {
    agent any
    environment {
        myVersion = '0.9'
    }
    stages {
        stage('restore') {
            steps {
                bat 'dotnet restore --configfile NuGet.Config'
            }
        }
        stage('build') {
            steps {
                bat 'dotnet build'
            }
        }
    }
}
