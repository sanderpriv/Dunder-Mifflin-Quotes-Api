package no.dunder.mifflin.dundermifflinquotes

import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.runApplication

@SpringBootApplication
class DunderMifflinQuotesApplication

fun main(args: Array<String>) {
    runApplication<DunderMifflinQuotesApplication>(*args)
}
