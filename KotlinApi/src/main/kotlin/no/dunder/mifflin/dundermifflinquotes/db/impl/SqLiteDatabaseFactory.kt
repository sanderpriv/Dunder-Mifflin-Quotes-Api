package no.dunder.mifflin.dundermifflinquotes.db.impl

import no.dunder.mifflin.dundermifflinquotes.service.impl.ResourceService.Companion.sqLiteDbSetFilename
import org.jetbrains.exposed.sql.Database
import org.jetbrains.exposed.sql.SchemaUtils
import org.jetbrains.exposed.sql.transactions.TransactionManager
import org.jetbrains.exposed.sql.transactions.transaction
import java.sql.Connection

object SqLiteDatabaseFactory {

    fun init() {

        val url = String.format("jdbc:sqlite:./%s", sqLiteDbSetFilename)
        Database.connect(url, "org.sqlite.JDBC")
        TransactionManager.manager.defaultIsolationLevel = Connection.TRANSACTION_SERIALIZABLE

        transaction {
            SchemaUtils.create(QuotesTable)
        }
    }
}