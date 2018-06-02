pipeline {
    agent any
    environment {
        myVersion = '0.9'
    }
    stages {
        stage('restore') {
            steps {
                sh 'dotnet restore'
            }
        }
        stage('build') {
            steps {
                sh 'dotnet build'
            }
        }
    }
}
