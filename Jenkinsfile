pipeline {
    agent any
    environment {
        myVersion = '0.9'
    }
    stages {
        stage('build') {
            steps {
                sh 'docker-compose -f ./docker-compose.ci.build.yml up'
            }
        }
    }
}
