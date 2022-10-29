package no.dunder.mifflin.dundermifflinquotes

import no.dunder.mifflin.dundermifflinquotes.db.IQuotesRepository
import no.dunder.mifflin.dundermifflinquotes.db.impl.SqLiteDatabaseFactory
import org.springframework.context.ApplicationListener
import org.springframework.context.event.ContextRefreshedEvent
import org.springframework.stereotype.Component

@Component
class ApplicationStartup(private val quotesRepository: IQuotesRepository) : ApplicationListener<ContextRefreshedEvent> {

    override fun onApplicationEvent(event: ContextRefreshedEvent) {
        SqLiteDatabaseFactory.init()
        quotesRepository.loadCsv()
    }
}