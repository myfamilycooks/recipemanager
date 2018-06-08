pipeline {
    agent any
    environment {
        myVersion = '0.9'
    }
    stages {
        stage('build api') {
            steps {
                sh 'docker-compose -f ./docker-compose.ci.build.yml up'
                sh 'sudo chown -R jenkins:jenkins .'
            }
        }
        stage('build ui') {
            steps {
                sh 'pwd'
            }
        }
    }
}