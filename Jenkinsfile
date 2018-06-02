pipeline {
    agent any
    environment {
        myVersion = '0.9'
    }
    tools {
        msbuild '.NET Core 2.0.0'
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
        stage('publish') {
            steps {
              ...
            }
        }
    }
}
