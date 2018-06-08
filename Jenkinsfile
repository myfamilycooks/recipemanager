pipeline {
    agent any
    environment {
        myVersion = '0.9'
    }
    stages {
        stage('build api') {
            steps {
                sh 'docker-compose -f ./docker-compose.ci.build.yml up'
            }
        }
        stage('build ui') {
            steps {
                sh 'pwd'
            }
        }
    }
}
