pipeline {
    agent any
    environment {
        myVersion = '0.9'
    }
    stages {
        stage('restore') {
            steps {
                sh 'dotnet restore ./BistroFiftyTwo.NoDocker.sln'
            }
        }
        stage('build') {
            steps {
                sh 'dotnet build ./BistroFiftyTwo.NoDocker.sln'
            }
        }
    }
}
