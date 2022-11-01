package no.dunder.mifflin.dundermifflinquotes.db.impl

import no.dunder.mifflin.dundermifflinquotes.db.IQuotesRepository
import no.dunder.mifflin.dundermifflinquotes.domain.dto.Quote
import no.dunder.mifflin.dundermifflinquotes.service.IResourceService
import org.jetbrains.exposed.sql.*
import org.jetbrains.exposed.sql.SqlExpressionBuilder.eq
import org.jetbrains.exposed.sql.transactions.transaction
import org.slf4j.Logger
import org.slf4j.LoggerFactory
import org.springframework.stereotype.Repository

@Repository
class SqLiteQuotesRepository(private val resourceService: IResourceService) : IQuotesRepository {

    private val logger: Logger = LoggerFactory.getLogger(SqLiteQuotesRepository::class.java)

    override fun create(quote: Quote) {
        transaction {
            QuotesTable.insert {
                it[quoteId] = quote.id
                it[season] = quote.season
                it[episode] = quote.episode
                it[scene] = quote.scene
                it[lineText] = quote.lineText
                it[speaker] = quote.speaker
                it[deleted] = quote.deleted
            }
        }
    }

    override fun createMany(quotes: List<Quote>) {
        transaction {
            QuotesTable.batchInsert(quotes) { q ->
                this[QuotesTable.quoteId] = q.id
                this[QuotesTable.season] = q.season
                this[QuotesTable.episode] = q.episode
                this[QuotesTable.scene] = q.scene
                this[QuotesTable.lineText] = q.lineText
                this[QuotesTable.speaker] = q.speaker
                this[QuotesTable.deleted] = q.deleted
            }
        }
    }

    override fun getAll(): List<Quote> {
        return transaction {
            QuotesTable.selectAll().map(::rowToQuote)
        }
    }

    override fun get(id: Int): Quote? {
        return transaction {
            QuotesTable.select( QuotesTable.quoteId eq id).map(::rowToQuote).getOrNull(0)
        }
    }

    override fun update(quote: Quote) {
        transaction {
            QuotesTable.update {
                it[quoteId] = quote.id
                it[season] = quote.season
                it[episode] = quote.episode
                it[scene] = quote.scene
                it[lineText] = quote.lineText
                it[speaker] = quote.speaker
                it[deleted] = quote.deleted
            }
        }
    }

    override fun delete(id: Int) {
        transaction {
            QuotesTable.deleteWhere { quoteId eq id }
        }
    }

    override fun loadCsv() {
        logger.info("Loading csv into database")

        if (getAll().isNotEmpty()) {
            logger.info("Database already has records, skipping loading csv into database")
            return
        }

        val quotes = resourceService.getQuotesFromCsvFile()
        logger.info("Loading quotes database")
        createMany(quotes)
        logger.info("Finished quotes database")
    }

    private fun rowToQuote(row: ResultRow): Quote {
        return Quote(
            id = row[QuotesTable.quoteId],
            season = row[QuotesTable.season],
            episode = row[QuotesTable.episode],
            scene = row[QuotesTable.scene],
            lineText = row[QuotesTable.lineText],
            speaker = row[QuotesTable.speaker],
            deleted = row[QuotesTable.deleted]
        )
    }
}